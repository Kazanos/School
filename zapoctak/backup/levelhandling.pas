unit levelhandling;

{$mode objfpc}{$H+}


interface

uses
  Classes, SysUtils, typehandling, playerhandling, functionhandling;

procedure loadlevel(path: string);
procedure savelevel(path: string);
procedure writelevel();
procedure movecharacter(x, y: Integer);
procedure fight(index: Integer);
procedure buyweapon();
procedure buyhealth();


implementation

//Loads map from file to record
procedure loadlevel(path: string);
var
  i, j: integer;
  temp: char;
  r: textfile;
begin
  Assign(r, 'data/'+path+'.txt');
  reset(r);
  readln(r, level^.Height);
  readln(r, level^.Width);
  setlength(level^.area, level^.Height + 1, level^.Width + 1);
  setlength(level^.monsters, 0);
  for i := 1 to level^.Height do
  begin
    for j := 1 to level^.Width do
    begin
      Read(r, temp);
      if temp = 'P' then
      begin
        player^.x := j;
        player^.y := i;
      end;
      if temp = 'M' then
      begin
        setlength(level^.monsters, length(level^.monsters) + 1);
        new(level^.monsters[length(level^.monsters)-1]);
        level^.monsters[length(level^.monsters)-1]^.x := j;
        level^.monsters[length(level^.monsters)-1]^.y := i;
        level^.monsters[length(level^.monsters)-1]^.bounty := 2;
        level^.monsters[length(level^.monsters)-1]^.health := 25;
        level^.monsters[length(level^.monsters)-1]^.damage := 4;
      end;
      level^.area[i][j] := temp;
    end;
    readln(r);
  end;
  readln(r, player^.health);
  readln(r, player^.damage);
  readln(r, player^.gold);
  readln(r, player^.level);
  Close(r);
end;

//Saves map from record to file
procedure savelevel(path: string);
var
  i, j: integer;
  w: textfile;
begin
  Assign(w, 'data/'+path+'.txt');
  rewrite(w);
  writeln(w, level^.Height);
  writeln(w, level^.Width);
  for i := 1 to level^.Height do
  begin
    for j := 1 to level^.Width do
      Write(w, level^.area[i][j]);
    writeln(w);
  end;
  writeln(w, player^.health);
  writeln(w, player^.damage);
  writeln(w, player^.gold);
  writeln(w, player^.level);
  Close(w);
end;

//Writes map to console
procedure writelevel();
var
  i, j: integer;
begin
  for i := 1 to level^.Height do
  begin
    for j := 1 to level^.Width do
    begin
      Write(level^.area[i][j]: 2);
    end;
    writeln;
  end;
end;

//Moves the player on the map
procedure movecharacter(x, y: Integer);
begin
  level^.area[player^.y][player^.x] := ' ';
  level^.area[player^.y + y][player^.x + x] := 'P';
  teleportplayer(player^.x + x, player^.y + y)
end;

//Fights the monster of the specified index in the array of monsters
procedure fight(index: Integer);
begin
  level^.monsters[index]^.health := level^.monsters[index]^.health - player^.damage;
  writeln('You have dealt ' + inttostr(player^.damage) +
          ' damage (monster has ' + inttostr(level^.monsters[index]^.health) + ' health left)');
  if level^.monsters[index]^.health > 0 then
  begin
    damageplayer(level^.monsters[index]^.damage)
  end
  else
  begin
    writeln('Congratulations, you have killed the monster');
    gaingold(level^.monsters[index]^.bounty);
    level^.area[level^.monsters[index]^.y][level^.monsters[index]^.x] := ' ';
    RemoveMonster(index);
    writelevel;
  end;
end;

//Buys a weapon (damage upgrade) if the player has enough gold
procedure buyweapon();
begin
  if player^.gold >= 3 then
  begin
    losegold(3);
    gaindamage(2);
  end
  else
  begin
    writeln('You need 3 gold but have only ' + IntToStr(player^.gold));
  end;
end;

//Buys health if the player has enough gold
procedure buyhealth();
begin
  if player^.gold >= 1 then
  begin
    losegold(1);
    gainhealth(7);
  end
  else
  begin
    writeln('You need 1 gold but have only ' + IntToStr(player^.gold));
  end;
end;

end.
