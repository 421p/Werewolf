<?php

use Psr\Http\Message\ServerRequestInterface;
use React\Http\Response;
use React\Http\Server;
use Symfony\Component\Yaml\Yaml;

require __DIR__.'/vendor/autoload.php';

$loop = React\EventLoop\Factory::create();

$server = new Server(
    function (ServerRequestInterface $request) {

        $yaml = Yaml::parse($request->getBody()->getContents());

        $language = $yaml['language'];
        $strings = $yaml['strings'];

        echo 'Converting language: '.json_encode($language, JSON_UNESCAPED_UNICODE).PHP_EOL;

        $xml = new DOMDocument();

        $xml->loadXML('<strings></strings>');

        $body = $xml->documentElement;

        $languageNode = $xml->createDocumentFragment();
        $languageNode->appendXML(sprintf(
            '<language name="%s" base="%s" variant="%s"/>',
            $language['name'],
            $language['base'],
            $language['variant']
        ));

        $body->appendChild($languageNode);

        foreach ($strings as $key => $values) {
            $child = sprintf('<string key="%s">', $key);

            foreach ($values as $value) {
                $child .= sprintf('<value>%s</value>', $value);
            }

            $child .= '</string>';

            $stringNode = $xml->createDocumentFragment();
            $stringNode->appendXML($child);

            $body->appendChild($stringNode);
        }

        return new Response(
            200,
            [
                'Content-Type' => 'application/xml',
            ],
            html_entity_decode($xml->saveXML())
        );
    }
);

$server->on('error', function (Exception $e) {
    echo 'Error: ' . $e->getMessage() . PHP_EOL;

    if ($e->getPrevious() !== null) {
        $previousException = $e->getPrevious();
        echo $previousException->getMessage() . PHP_EOL;
    }
});

$socket = new React\Socket\Server('0.0.0.0:8080', $loop);
$server->listen($socket);

echo "Server started.".PHP_EOL;

$loop->run();