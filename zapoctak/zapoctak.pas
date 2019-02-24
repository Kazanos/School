program Kingswood;

uses
  Classes,
  SysUtils,
  commandhandling,
  levelhandling;

var
  playing: boolean;
  Text: string;

begin
  start();
  playing := True;
  while playing do
  begin
    writeln('What do you wish to do? (move, fight, buy, save, load)');
    readln(Text);
    undergo(Text);
  end;
  readln;
  readln;
end.
