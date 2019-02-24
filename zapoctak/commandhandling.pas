unit commandhandling;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, levelhandling, typehandling;
procedure start();
procedure undergo(text:String);
procedure askload();
procedure asksave();
procedure attemptmove();
procedure attemptfight();
procedure attemptbuy();

implementation

//Prepares the game
procedure start();
begin
  new(level);
  new(player);
  askload;
end;

//Command crossroads
procedure undergo(text:String);
begin
  case text of
  'load', 'l': askload;
  'save', 's': asksave;
  'move','m': attemptmove;
  'fight', 'f': attemptfight;
  'buy', 'b': attemptbuy;
  end;
end;

//Loads what the player wants to load
procedure askload();
var
  path:String;
begin
  writeln('Do you want to load a saved game?');
  readln(path);
  if ((path = 'Yes') or (path = 'yes')) then
    begin
      writeln('Please enter the name of the desired save:');
      readln(path);
      loadlevel('saves/'+path);
    end
                                  else
    begin
      writeln('Please enter the name of the desired map:');
      readln(path);
      loadlevel(path);
    end;
    writelevel;
end;

//Saves the game with a player-chosen name
procedure asksave();
var
  path:String;
begin
  writeln('Please enter the desired name of your save:');
  readln(path);
  savelevel('saves/'+path);
end;

//Attempts to move the player in a specified direction
procedure attemptmove();
var
  direction:String;
begin
  writeln('Where would you like to move? (up, down, left, right)');
  readln(direction);
  case direction of
  'up', 'u': if level^.area[player^.y - 1][player^.x] = ' ' then movecharacter(0, -1)
                                                       else writeln('Cannot move there');
  'down', 'd': if level^.area[player^.y + 1][player^.x] = ' ' then movecharacter(0, 1)
                                                         else writeln('Cannot move there');
  'left', 'l': if level^.area[player^.y][player^.x - 1] = ' ' then movecharacter(-1, 0)
                                                         else writeln('Cannot move there');
  'right', 'r': if level^.area[player^.y][player^.x + 1] = ' ' then movecharacter(1, 0)
                                                          else writeln('Cannot move there');
  end;
  writelevel;
end;

//Attemps to fight an adjacent monster (First clockwise from the top in case of multiple enemies)
procedure attemptfight();
var
  monsterx, monstery, index, i: Integer;
begin
  monsterx := 0;
  monstery := 0;

  if level^.area[player^.y - 1][player^.x] = 'M' then begin monsterx := player^.x; monstery := player^.y - 1 end
  else if level^.area[player^.y][player^.x + 1] = 'M' then begin monsterx := player^.x + 1; monstery := player^.y end
  else if level^.area[player^.y + 1][player^.x] = 'M' then begin monsterx := player^.x; monstery := player^.y + 1 end
  else if level^.area[player^.y][player^.x - 1] = 'M' then begin monsterx := player^.x - 1; monstery := player^.y end
  else writeln('No monsters nearby');

  if (monsterx <> 0) and (monstery <> 0) then
    begin
      index := 0;
      i := 0;
      while index = 0 do
      begin
        writeln(i);
        if ((level^.monsters[i]^.x = monsterx) and (level^.monsters[i]^.y = monstery)) then
          begin
            index := i;
          end;
        i := i + 1;
      end;
      fight(index);
    end;
end;

//Attempts to buy from an adjacent merchant
procedure attemptbuy();
var
  shopx, shopy: Integer;
begin
  shopx := 0;
  shopy := 0;
  if (level^.area[player^.y - 1][player^.x] = 'B') or (level^.area[player^.y - 1][player^.x] = 'H') then
    begin
      shopx := player^.x;
      shopy := player^.y - 1;
    end
  else if (level^.area[player^.y][player^.x + 1] = 'B') or (level^.area[player^.y][player^.x + 1] = 'H') then
    begin
      shopx := player^.x + 1;
      shopy := player^.y;
    end
  else if (level^.area[player^.y + 1][player^.x] = 'B') or (level^.area[player^.y + 1][player^.x] = 'H') then
    begin
      shopx := player^.x;
      shopy := player^.y + 1;
    end
  else if (level^.area[player^.y][player^.x - 1] = 'B') or (level^.area[player^.y][player^.x - 1] = 'H') then
    begin
      shopx := player^.x - 1;
      shopy := player^.y;
    end;
  if (shopx <> 0) and (shopy <> 0) then
    begin
      if level^.area[shopy][shopx] = 'B' then
        buyweapon()
      else
        buyhealth();
    end;

end;

end.

