FROM mono

RUN mkdir -p /etc/mono/registry/LocalMachine/software/werewolf/
RUN chmod uog+rw /etc/mono/registry

ADD ["RegistrySaver/bin/Release/RegistrySaver.exe", "/rsaver"]
ADD entrypoint.sh /entrypoint

ADD ["WerewolfControl/bin/Release", "/app/goose/Control"]
ADD Languages /app/Languages
ADD ["WerewolfNode/bin/Release", "/app/goose/Node"]

RUN mkdir /app/goose/Logs

WORKDIR /app/goose/Control

ENTRYPOINT ["/bin/bash", "/entrypoint"]