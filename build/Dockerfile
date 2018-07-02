FROM mono as builder

ADD . /build

WORKDIR /build

RUN nuget restore
RUN msbuild /p:Configuration=Release

FROM mono

COPY --from=builder /build/WerewolfControl/bin/Release /app/goose/Control
COPY --from=builder /build/Languages /app/Languages
COPY --from=builder /build/WerewolfNode/bin/Release /app/goose/Node

RUN mkdir /app/goose/Logs

WORKDIR /app/goose/Control

ENTRYPOINT ["/usr/bin/mono", "/app/goose/Control/WerewolfControl.exe"]