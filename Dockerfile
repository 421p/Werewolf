FROM mono

ADD ["WerewolfControl/bin/Release", "/app/goose/Control"]
ADD Languages /app/Languages
ADD ["WerewolfNode/bin/Release", "/app/goose/Node"]

RUN mkdir /app/goose/Logs

WORKDIR /app/goose/Control

ENTRYPOINT ["/usr/bin/mono", "/app/goose/Control/WerewolfControl.exe"]