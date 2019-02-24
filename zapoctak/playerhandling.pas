unit playerhandling;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, typehandling, functionhandling;

procedure gaingold(amount: integer);
procedure losegold(amount: integer);
procedure healplayer(amount: integer);
procedure damageplayer(amount: integer);
procedure teleportplayer(x, y: integer);
procedure gaindamage(amount: integer);

implementation

//Gives the player the specified amount of gold
procedure gaingold(amount: integer);
begin
  player^.gold := player^.gold + amount;
  writeln('You have gained ' + IntToStr(amount) + ' gold (You now have ' +
    IntToStr(player^.gold) + ' gold)');
end;

//Removes the specified amount of gold from the player
procedure losegold(amount: integer);
begin
  player^.gold := player^.gold - amount;
  writeln('You have lost ' + IntToStr(amount) + ' gold (You now have ' +
    IntToStr(player^.gold) + ' gold)');
end;

//Increases the players health by the specified amount
procedure healplayer(amount: integer);
begin
  player^.health := player^.health + amount;
  writeln('You have gained ' + IntToStr(amount) + ' health (You now have ' +
    IntToStr(player^.health) + ')');
end;

//Reduces the players health by the specified amount
procedure damageplayer(amount: integer);
begin
  player^.health := player^.health - amount;
  writeln('You have taken ' + IntToStr(amount) + ' damage and have ' +
    IntToStr(player^.health) + ' health remaining');
end;

//Sets player coordinates to a specific value
procedure teleportplayer(x, y: integer);
begin
  player^.x := x;
  player^.y := y;
end;

//Increases the players damage by the specified amount
procedure gaindamage(amount: integer);
begin
  Write('Your damage has increased from ' + IntToStr(player^.damage) + ' to ');
  player^.damage := player^.damage + amount;
  writeln(IntToStr(player^.damage));
end;

end.
