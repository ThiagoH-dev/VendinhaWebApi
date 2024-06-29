# Olá,
## aqui explicarei o funcionamento das entidades.

ORM: Nhibernate

Clientes (cadastro, recuperação, atualização e exclusão)
	- Nome completo *
	- CPF válido *
	- Data de nascimento *
	- Email
	- bag de dividas penduradas (adicionavel)
	(essas dividas não podem ser maiores que 200 reais)

Dividas (cadastraveis e removiveis)
	- Valor *
	- Situação (paga, em andamento e vencida) *
	- Data da criação (automatico) *
		- possível de criar com data mais antiga
	- Data de pagamento (opcional)
		- (Limite/Data que foi paga)
		- (anulavel)
	- Descrição (produto)

Relação (dado extra)
	- Clientes 
	(caso um grupo de clientes tenha a mesma divida)
	- Divida