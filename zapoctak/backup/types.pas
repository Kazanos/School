unit typehandling;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils;

implementation
 type
  TLevel:record
             area: Array of Array of Char;
             width: Integer;
             height: Integer;
  PLevel:^TLevel;
end.

