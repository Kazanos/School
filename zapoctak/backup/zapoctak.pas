program Kingswood;

uses
  Classes,
  SysUtils, commandhandling, levelhandling;

var
  playing: Boolean;
  text: String;

begin
  start();
  playing := true;
  while playing do
  begin
    writeln('What do you wish to do? (move, fight, buy, save, load)');
    readln(text);
    undergo(text);
  end;
  readln;
  readln;
end.
