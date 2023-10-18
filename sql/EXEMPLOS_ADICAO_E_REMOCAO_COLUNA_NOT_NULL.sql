/*
	MOTIVA��O:
		As vezes voc� trabalha em um ambiente que n�o usa nenhuma ferramenta de migration ou algo como SQL Data Tools 
		e precisa fazer as altera��es no banco via GMUD. Al�m disso precisa de um script de rollback para desfazer 
		suas altera��es na ordem reversa que elas foram feitas.

		Esse tipo de altera��o � dif�cil quando voc� precisa adicionar um novo campo com not null mas j� existem 
		registros na tabela. Voc� deve adicionar o campo com um default, mas n�o deve deixar o default autom�tico
		sem nome de constraint, porque o SQL server vai criar um nome autom�tico para essa constraint e na hora de 
		remover essa coluna voc� n�o vai consegur fazer o drop column porque ela tem uma constraint e voc� n�o vai 
		conseguir remover essa constraint~via script porque n�o sabe o nome dela no servidor de produ��o.

	

	O QUE VEREMOS AQUI:
		- Como adicionar novas colunas not null em uma tabela com registros existentes
		- Como adicionar uma constraint de default nomeada
		- Como remover uma constraint
		- Como remover uma coluna que tenha default
		- Como modificar uma coluna pr� existente para que seja not null com default. 
*/

/***********************************************************************************************************************
*************************************** CRIA��O DA TABELA E ADI��O DE REGISTROS   **************************************
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
*************************************** M�TODO 1 - COLUNA INEXISTENTE NOT NULL *****************************************
************************************************************************************************************************/

-- pra adicionar se o campo n�o existe
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
*************************************** M�TODO 2 - COLUNA INEXISTENTE NOT NULL *****************************************
************************************************************************************************************************/
-- pra adicionar se o campo n�o existe
alter table COBAIA add  COLUNA_NOVA_2 int not null  constraint DF_COLUNA_NOVA_2 default 0   --TIRAMOS A V�RGULA E OMITIMOS O FOR

-- PRA TESTAR
SELECT * FROM COBAIA

-- pra dropar default e depois dropar coluna
alter table COBAIA drop constraint DF_COLUNA_NOVA_2
alter table COBAIA drop column COLUNA_NOVA_2
/***********************************************************************************************************************
*************************************** FIM ************************* **************************************************
************************************************************************************************************************/





/***********************************************************************************************************************
*************************************** M�TODO 3 - COLUNA INEXISTENTE **************************************************
************************************************************************************************************************/
--https://stackoverflow.com/questions/92082/add-a-column-with-a-default-value-to-an-existing-table-in-sql-server

-- pra adicionar se o campo n�o existe e voc� quer que seja nul�vel mas que j� entrem os valores com default
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
*************************************** M�TODO 4 - COLUNA PR� EXISTENTE COM NULLS **************************************
************************************************************************************************************************/
--ESSE M�TODO � PRA QUANDO A COLUNA J� EXISTE E TEM VALORES NULL, VOC� QUER TORNAR ELA NOT NULL E COLOCAR OS VALORES DEFAULT

-- pra adicionar default se o campo j� existe e � null (sem with values)
alter table COBAIA add constraint DF_COLUNA_EXISTENTE_1 default 0 for COLUNA_EXISTENTE_1

/*
necess�rio se voc� n�o usar o with values, 
se n�o fizer isso a pr�xima linha d� erro 
porque ele n�o consegue inserir null (j� existente) em uma coluna not null
*/
update COBAIA set COLUNA_EXISTENTE_1 = 0 where COLUNA_EXISTENTE_1 is null 

alter table COBAIA alter column COLUNA_EXISTENTE_1 int not null 

-- PRA TESTAR
SELECT * FROM COBAIA

-- pra dropar default e depois dropar coluna
alter table COBAIA drop constraint DF_COLUNA_EXISTENTE_1
--N�O VAMOS DROPAR A COLUNA COLUNA_EXISTENTE_1 PORQUE ELA EXISTIA ANTES DA ALTERA��O
alter table COBAIA alter column COLUNA_EXISTENTE_1 int null
update COBAIA set COLUNA_EXISTENTE_1 = null where COLUNA_EXISTENTE_1 = 0

/***********************************************************************************************************************
*************************************** FIM ************************* **************************************************
************************************************************************************************************************/










