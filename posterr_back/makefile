# Makefile

# Alvo (target) para subir os serviços necessários para a infraestrutura local
.PHONY: infra-local
infra-local:
	docker-compose up -d posterr-db

# Alvo (target) para executar as migrações
.PHONY: run-migrations
run-migrations:
	docker-compose up --abort-on-container-exit --exit-code-from posterr-migrations

# Alvo (target) para executar ambos os alvos acima sequencialmente
.PHONY: setup-local
setup-local: infra-local run-migrations

# Alvo (target) para descer os serviços
.PHONY: down
down:
	docker-compose down
