# Ol�,
## aqui explicarei o funcionamento das entidades.

ORM: Nhibernate

Clientes (cadastro, recupera��o, atualiza��o e exclus�o)
	- Nome completo *
	- CPF v�lido *
	- Data de nascimento *
	- Email
	- bag de dividas penduradas (adicionavel)
	(essas dividas n�o podem ser maiores que 200 reais)

Dividas (cadastraveis e removiveis)
	- Valor *
	- Situa��o (paga, em andamento e vencida) *
	- Data da cria��o (automatico) *
		- poss�vel de criar com data mais antiga
	- Data de pagamento (opcional)
		- (Limite/Data que foi paga)
		- (anulavel)
	- Descri��o (produto)

Rela��o (dado extra)
	- Clientes 
	(caso um grupo de clientes tenha a mesma divida)
	- Divida