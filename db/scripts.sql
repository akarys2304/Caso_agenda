--Criação da tabela 
-- Instruções para a criação do banco: 
-- host = postgres
-- user = postgres
-- password = 12345678
-- porta = 5432

--Ele vai pegar automaticamente a versão do postgres pelo docker compose 

CREATE TABLE public.contatos (
	id serial4 NOT NULL,
	nome varchar NOT NULL,
	telefone varchar NOT NULL,
	CONSTRAINT contatos_pkey PRIMARY KEY (id)
);

-- Como o Id é serial, não precisa sempre digitar o id, o que vai ser muito útil para nosso formulário
INSERT INTO public.contatos (nome, telefone) VALUES('Aline Manhães', '(12) 12345-2345');

--Script de alteração de tabela
UPDATE public.contatos
SET nome='Aline Manhães', telefone='(12) 12345-2346'
WHERE id=2;

--Script de deleção 
DELETE FROM public.contatos
WHERE id=3;  --Deleta o Ferdinando Touro


-- Seleciona todos
SELECT * FROM public.contatos;

-- Seleciona alguém em específico
SELECT * FROM public.contatos WHERE id=2; --Seleciona Karys aqui