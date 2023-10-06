/*
	MOTIVAÇÃO:
		As vezes você trabalha em um ambiente que não usa nenhuma ferramenta de migration ou algo como SQL Data Tools 
		e precisa fazer as alterações no banco via GMUD. Além disso precisa de um script de rollback para desfazer 
		suas alterações na ordem reversa que elas foram feitas.

		Esse tipo de alteração é difícil quando você precisa adicionar um novo campo com not null mas já existem 
		registros na tabela. Você deve adicionar o campo com um default, mas não deve deixar o default automático
		sem nome de constraint, porque o SQL server vai criar um nome automático para essa constraint e na hora de 
		remover essa coluna você não vai consegur fazer o drop column porque ela tem uma constraint e você não vai 
		conseguir remover essa constraint~via script porque não sabe o nome dela no servidor de produção.

	

	O QUE VEREMOS AQUI:
		- Como adicionar novas colunas not null em uma tabela com registros existentes
		- Como adicionar uma constraint de default nomeada
		- Como remover uma constraint
		- Como remover uma coluna que tenha default
		- Como modificar uma coluna pré existente para que seja not null com default. 
*/

/***********************************************************************************************************************
*************************************** CRIAÇÃO DA TABELA E ADIÇÃO DE REGISTROS   **************************************
************************************************************************************************************************/
--drop table COBAIA
create table COBAIA
(
	ID int IDENTITY(1,1) not null,
	CONSTRAINT PK_COBAIA PRIMARY KEY CLUSTERED (ID),
	NOME VARCHAR(20) NULL,
	COLUNA_EXISTENTE_1 INT NULL,
	COLUNA_EXISTENTE_2 INT NULL
)
INSERT INTO COBAIA (NOME) VALUES ('TESTE1')
INSERT INTO COBAIA (NOME) VALUES ('TESTE2')
INSERT INTO COBAIA (NOME, COLUNA_EXISTENTE_1) VALUES ('TESTE3', 3)
INSERT INTO COBAIA (NOME, COLUNA_EXISTENTE_2) VALUES ('TESTE4', 4)

SELECT * FROM COBAIA
/***********************************************************************************************************************
*************************************** FIM ************************* **************************************************
************************************************************************************************************************/





/***********************************************************************************************************************
*************************************** MÉTODO 1 - COLUNA INEXISTENTE NOT NULL *****************************************
************************************************************************************************************************/

-- pra adicionar se o campo não existe
alter table COBAIA add  COLUNA_NOVA_1 int not null,  constraint DF_COLUNA_NOVA_1 default 0 for COLUNA_NOVA_1

-- PRA TESTAR
SELECT * FROM COBAIA

-- pra dropar default e depois dropar coluna
alter table COBAIA drop constraint DF_COLUNA_NOVA_1
alter table COBAIA drop column COLUNA_NOVA_1

/***********************************************************************************************************************
*************************************** FIM ************************* **************************************************
************************************************************************************************************************/








/***********************************************************************************************************************
*************************************** MÉTODO 2 - COLUNA INEXISTENTE NOT NULL *****************************************
************************************************************************************************************************/
-- pra adicionar se o campo não existe
alter table COBAIA add  COLUNA_NOVA_2 int not null  constraint DF_COLUNA_NOVA_2 default 0   --TIRAMOS A VÍRGULA E OMITIMOS O FOR

-- PRA TESTAR
SELECT * FROM COBAIA

-- pra dropar default e depois dropar coluna
alter table COBAIA drop constraint DF_COLUNA_NOVA_2
alter table COBAIA drop column COLUNA_NOVA_2
/***********************************************************************************************************************
*************************************** FIM ************************* **************************************************
************************************************************************************************************************/





/***********************************************************************************************************************
*************************************** MÉTODO 3 - COLUNA INEXISTENTE **************************************************
************************************************************************************************************************/
--https://stackoverflow.com/questions/92082/add-a-column-with-a-default-value-to-an-existing-table-in-sql-server

-- pra adicionar se o campo não existe e você quer que seja nulável mas que já entrem os valores com default
alter table COBAIA add  COLUNA_NOVA_3 int   constraint DF_COLUNA_NOVA_3 default 0   with values

-- PRA TESTAR
SELECT * FROM COBAIA

-- pra dropar default e depois dropar coluna
alter table COBAIA drop constraint DF_COLUNA_NOVA_3
alter table COBAIA drop column COLUNA_NOVA_3

/***********************************************************************************************************************
*************************************** FIM ************************* **************************************************
************************************************************************************************************************/




/***********************************************************************************************************************
*************************************** MÉTODO 4 - COLUNA PRÉ EXISTENTE COM NULLS **************************************
************************************************************************************************************************/
--ESSE MÉTODO É PRA QUANDO A COLUNA JÁ EXISTE E TEM VALORES NULL, VOCÊ QUER TORNAR ELA NOT NULL E COLOCAR OS VALORES DEFAULT

-- pra adicionar default se o campo já existe e é null (sem with values)
alter table COBAIA add constraint DF_COLUNA_EXISTENTE_1 default 0 for COLUNA_EXISTENTE_1

/*
necessário se você não usar o with values, 
se não fizer isso a próxima linha dá erro 
porque ele não consegue inserir null (já existente) em uma coluna not null
*/
update COBAIA set COLUNA_EXISTENTE_1 = 0 where COLUNA_EXISTENTE_1 is null 

alter table COBAIA alter column COLUNA_EXISTENTE_1 int not null 

-- PRA TESTAR
SELECT * FROM COBAIA

-- pra dropar default e depois dropar coluna
alter table COBAIA drop constraint DF_COLUNA_EXISTENTE_1
--NÃO VAMOS DROPAR A COLUNA COLUNA_EXISTENTE_1 PORQUE ELA EXISTIA ANTES DA ALTERAÇÃO
alter table COBAIA alter column COLUNA_EXISTENTE_1 int null
update COBAIA set COLUNA_EXISTENTE_1 = null where COLUNA_EXISTENTE_1 = 0

/***********************************************************************************************************************
*************************************** FIM ************************* **************************************************
************************************************************************************************************************/










