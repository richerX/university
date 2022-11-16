using System;

[Flags]
public enum Permissions
{
    Default,
    UserRead,
    UserWrite,
    UserExecute,
    GroupRead,
    GroupWrite,
    GroupExecute,
    EveryoneRead,
    EveryoneWrite,
    EveryoneExecute
}