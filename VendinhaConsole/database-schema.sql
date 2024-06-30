drop table clientes

drop table dividas

drop table relacoes

drop sequence clientes_seq

drop sequence dividas_seq

create table clientes (
    Id int4 not null,
    CPF varchar(11),
    Nome varchar(50),
    Email varchar(50),
    DataNascimento date,
    primary key (Id)
)

create table dividas (
    Id int4 not null,
    Valor float8,
    Descricao text,
    Pendente boolean,
    DataCriacao date,
    DataPagamento date,
    primary key (Id)
)

create table relacoes (
    Id int4 not null,
    Data timestamp,
    clientecodigo int4,
    dividaid int4,
    primary key (Id)
)

alter table relacoes
    add constraint FK_2A35345E
    foreign key (clientecodigo)
    references clientes

alter table relacoes
    add constraint FK_D6EA1540
    foreign key (dividaid)
    references dividas

create sequence clientes_seq

create sequence dividas_seq