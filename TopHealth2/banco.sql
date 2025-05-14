CREATE TABLE Alimentacao (
  id INTEGER PRIMARY KEY,
  Descricao TEXT NOT NULL,
  ValorEnergetico INTEGER NOT NULL
);

CREATE TABLE AtividadeFisica (
  id INTEGER PRIMARY KEY,
  TipoAtividade TEXT NOT NULL,
  DuracaoMinutos INTEGER NOT NULL
);

CREATE TABLE Configuracao (
  UserId INTEGER PRIMARY KEY,
  CaminhoBanco TEXT NOT NULL,
  Tema TEXT NOT NULL,
  FlagImportacao INTEGER NOT NULL
);

CREATE TABLE Humor (
  id INTEGER PRIMARY KEY,
  Descricao TEXT NOT NULL
);

CREATE TABLE QualidadeSono (
  id INTEGER PRIMARY KEY,
  Descricao TEXT NOT NULL
);

CREATE TABLE RegistroDiario (
  id INTEGER PRIMARY KEY,
  UserId INTEGER NOT NULL,
  Data TEXT NOT NULL, 
  HumorId INTEGER NOT NULL,
  SonoId INTEGER NOT NULL,
  AlimentacaoId INTEGER NOT NULL,
  AtividadeFisicaId INTEGER NOT NULL
);

CREATE TABLE Usuario (
  id INTEGER PRIMARY KEY,
  Nome TEXT NOT NULL,
  Sobrenome TEXT NOT NULL,
  email TEXT NOT NULL,
  senha TEXT NOT NULL
);

-- Inserts

INSERT INTO Alimentacao (id, Descricao, ValorEnergetico) VALUES
(0, 'Filet mignon aux demi-glace', 600);

INSERT INTO AtividadeFisica (id, TipoAtividade, DuracaoMinutos) VALUES
(0, 'corrida', 60);

INSERT INTO Configuracao (UserId, CaminhoBanco, Tema, FlagImportacao) VALUES
(0, 'arquivo onde ficará o banco de dados', 'escuro', 1);

INSERT INTO Humor (id, Descricao) VALUES
(0, 'feliz');

INSERT INTO QualidadeSono (id, Descricao) VALUES
(0, 'boa');

INSERT INTO RegistroDiario (id, UserId, Data, HumorId, SonoId, AlimentacaoId, AtividadeFisicaId) VALUES
(0, 0, '2025-04-30', 0, 0, 0, 0);

INSERT INTO Usuario (id, Nome, Sobrenome, email, senha) VALUES
(0, 'Exemplício', 'Exemplificador de Exemplos', 'genericsample@gmail.com', 'e8d95a51f3af4a3b134bf6bb680a213a');