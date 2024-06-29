
create table clientes(
	id integer not null,
	cpf varchar(12),
	nome varchar(100),
	datanascimento date,
	email varchar(200)
);

create sequence dividas_seq;
create sequence relacao_seq;

create table dividas(
	id integer not null default nextval('dividas_seq'),
	nome varchar(50),
	descricao text,
	nivel integer not null,
	duracao integer not null,
	primary key (id)
);

create table relacao(
	id integer not null default nextval('relacao_seq'),
	clientecodigo integer,
	dividaid integer,
	data timestamp default now()
);

alter table relacao 
add constraint fk_relacao_cliente foreign key (clientecodigo)
references clientes(id);

alter table relacao 
add constraint fk_relacao_divida foreign key (dividaid)
references dividas(id);


create sequence clientes_seq;

alter table clientes
add primary key (id);

alter table clientes
alter column id set default nextval('clientes_seq');