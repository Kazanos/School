unit functionhandling;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, typehandling;

procedure RemoveMonster(index: integer);
function IntToStr(number: integer): string;

implementation

//Removes the monster at the specified index
procedure RemoveMonster(index: integer);
var
  i: integer;
  p: PMonster;
begin
  p := level^.monsters[index];
  for i := index to (length(level^.monsters) - 1) do
  begin
    level^.monsters[i] := level^.monsters[i + 1];
  end;
  setlength(level^.monsters, length(level^.monsters) - 1);
  dispose(p);
end;

//converts an integer to a string
function IntToStr(number: integer): string;
var
  temp: string;
begin
  while number <> 0 do
  begin
    temp := chr(Ord('0') + (number mod 10)) + temp;
    number := number div 10;
  end;
  IntToStr := temp;
end;

end.
