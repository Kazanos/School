program Kingswood;

uses
  Classes,
  SysUtils, commandhandling, levelhandling, unit1;

var
  playing: Boolean;
  text: String;

begin
  start();
  playing := true;
  while playing do
  begin
    writeln('What do you wish to do? (move, fight, save, load)');
    readln(text);
    undergo(text);
  end;

  readln;
  readln;
end.
