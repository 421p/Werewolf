#!/usr/bin/env bash

function save() {
    /usr/bin/mono /rsaver "$1" "$2"
}

if [ ! -z ${BOTAN_TOKEN+isset} ]; then 
    echo 'Found BOTAN_TOKEN...'
    save 'BotanReleaseAPI' "$BOTAN_TOKEN"
else 
    echo "BOTAN_TOKEN is not set. Exiting."
    exit 1   
fi

if [ ! -z ${CONNECTION_STRING+isset} ]; then 
    echo 'Found CONNECTION_STRING...'
    save 'BotConnectionString' "$CONNECTION_STRING"    
else 
    echo "CONNECTION_STRING is not set. Exiting."
    exit 1   
fi

if [ ! -z ${TELEGRAM_TOKEN+isset} ]; then 
    echo 'Found TELEGRAM_TOKEN...'
    save 'ProductionAPI' "$TELEGRAM_TOKEN"
else 
    echo "TELEGRAM_TOKEN is not set. Exiting."
    exit 1   
fi

sed -i 's/&quot;/"/g' /etc/mono/registry/LocalMachine/software/werewolf/values.xml

echo "Starting bot..."

/usr/bin/mono /app/goose/Control/WerewolfControl.exe