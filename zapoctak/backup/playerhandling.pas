unit playerhandling;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, typehandling, functionhandling;

procedure gaingold(amount: Integer);
procedure losegold(amount: Integer);
procedure healplayer(amount: Integer);
procedure damageplayer(amount: Integer);
procedure teleportplayer(x, y: integer);
procedure gaindamage(amount: Integer);

implementation

//Gives the player the specified amount of gold
procedure gaingold(amount: Integer);
begin
  player^.gold := player^.gold + amount;
  writeln('You have gained ' + inttostr(amount) + ' gold (You now have ' + inttostr(player^.gold) + ' gold)')
end;

//Removes the specified amount of gold from the player
procedure losegold(amount: Integer);
begin
  player^.gold := player^.gold - amount;
  writeln('You have lost ' + inttostr(amount) + ' gold (You now have ' + inttostr(player^.gold) + ' gold)')
end;

//Increases the players health by the specified amount
procedure healplayer(amount: Integer);
begin
  player^.health := player^.health + amount;
  writeln('You have gained ' + inttostr(amount) + ' health (You now have ' + inttostr(player^.health) + ')');
end;

//Reduces the players health by the specified amount
procedure damageplayer(amount: Integer);
begin
  player^.health := player^.health - amount;
  writeln('You have taken ' + inttostr(amount) + ' damage and have ' + inttostr(player^.health) + ' health remaining')
end;

//Sets player coordinates to a specific value
procedure teleportplayer(x, y: Integer);
begin
  player^.x := x;
  player^.y := y;
end;

//Increases the players damage by the specified amount
procedure gaindamage(amount: Integer);
begin
  write('Your damage has increased from ' + inttostr(player^.damage) + ' to ');
  player^.damage := player^.damage + amount;
  writeln(IntToStr(player^.damage));
end;
end.

