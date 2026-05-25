--Criação da tabela 
-- Instruções para a criação do banco: 
-- host = postgres
-- user = postgres
-- password = 12345678
-- porta = 5432

--Ele vai pegar automaticamente a versão do postgres pelo docker compose 

CREATE TABLE public.Contatos (
	id serial4 NOT NULL,
	nome varchar NOT NULL,
	telefone varchar NOT NULL,
	CONSTRAINT contatos_pkey PRIMARY KEY (id)
);

-- Como o Id é serial, não precisa sempre digitar o id, o que vai ser muito útil para nosso formulário
INSERT INTO public."Contatos" (nome, telefone) VALUES('Aline Manhães', '5527999990000');
INSERT INTO public."Contatos" (nome, telefone) VALUES('Patricia', '5513997050180');
INSERT INTO public."Contatos" (nome, telefone) VALUES('Pitcho', '551633643003');
INSERT INTO public."Contatos" (nome, telefone) VALUES('bethany', '5511989898989');

--Script de alteração de tabela
UPDATE public."Contatos" SET nome='Aline Manhães', telefone='(12) 12345-2346' WHERE id=2;

--Script de deleção 
DELETE FROM public."Contatos" WHERE id=3; 

-- Seleciona todos
SELECT * FROM public."Contatos";

-- Seleciona alguém em específico
SELECT * FROM public."Contatos" WHERE id=2; 