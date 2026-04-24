;#DropDB InventoryProjectXPOEasyTest

#Application InventoryProjectXPO.Win
#Application InventoryProjectXPO.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef InventoryProjectXPO.Win

*Action About...
 
*Action OK

#EndIf

#IfDef InventoryProjectXPO.Web
*Action Navigation(My Details)
#EndIf
