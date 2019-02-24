unit typehandling;

{$mode objfpc}{$H+}

interface

type
  TPlayer = record
    level, health, gold, damage: integer;
    x, y: integer;
  end;
  PPlayer = ^TPlayer;

  TMonster = record
    health, damage, bounty: integer;
    x, y: integer;
  end;
  PMonster = ^TMonster;

  TLevel = record
    area: array of array of char;
    monsters: array of PMonster;
    Width, Height: integer;
  end;
  PLevel = ^TLevel;

var
  level: PLevel;
  player: PPlayer;

implementation

end.
