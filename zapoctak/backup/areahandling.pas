unit areahandling;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils;

implementation
procedure loadlevel(path:String;var area:Array of Array);
var
  i,j,height,width:Integer;
  temp:char;
  r:textfile;
begin
  assign(r,path);
  reset(r);
  readln(r,height);
  readln(r,width);
  setlength(area,height+1);
  for i:=1 to height do
    begin
      setlength(area[i],width+1);
      for j:=1 to width do
        begin
          read(r,temp);
          area[i][j]:= temp;
        end;
      readln(r);
    end;
  close(r);
end;

procedure savelevel(path:String;height,width:Integer);
var
  i,j:Integer;
  w:textfile;
begin
  assign(w,path);
  rewrite(w);
  for i:=1 to height do
    begin
      for j:=1 to width do
        write(w,area[i][j]);
      writeln(w);
    end;
  close(w);
end;

procedure writelevel;
var
  i,j:Integer;
begin
  for i:= 1 to height do
    begin
      for j:=1 to width do
        write(area[i][j]);
      writeln;
    end;
end;
end.

