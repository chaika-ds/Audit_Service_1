docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --create --topic dev.auditlog.transactions; 
docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --create --topic dev.system.permission
docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --create --topic dev.healthCheck