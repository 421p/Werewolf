FROM mono

RUN mkdir -p /etc/mono/registry/LocalMachine/software/werewolf/
ADD values.xml /etc/mono/registry/LocalMachine/software/werewolf/values.xml
RUN chmod uog+rw /etc/mono/registry

ADD ["WerewolfControl/bin/Release", "/app/goose/Control"]
ADD Languages /app/Languages
ADD ["WerewolfNode/bin/Release", "/app/goose/Node1"]

RUN mkdir /app/goose/Logs

WORKDIR /app/goose/Control

ENTRYPOINT ["/usr/bin/mono", "/app/goose/Control/WerewolfControl.exe"]