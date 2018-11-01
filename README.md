# Futurez.XrmTools.AlternateKeyManager
XrmToolbox Plugin: Alternate Key Manager control for Dynamics 365

## General Usage
The Alternate Key Manager allows you to load all Entities in an instance and then load the Alternate Keys for selected Entities from the list 

![Main Screen](https://futurezconsulting.com/wp-content/uploads/2018/10/Alternate-Key-Manager.png)

Once the Entities are loaded, check any Entities for which you would like to load the keys.  

NOTE: Selecting all on a system with a large number of Entities may take a minute or two to load.

## Interacting with Keys

If a selected Entity has one or more Alternate Keys defined, they will display in the list, grouped by the Entity display name.  The Status column indicates whether the key is in an Active state or has failed on creation. If an Alternate Key is in an error state, you can attempt to Activate the key using the tool button provided.

Selecting the Alternate Key from the list will display it's details in the right pane, such as the underlying schema name. 

## Creating new Alternate Keys

In the right pane, you can create a new Alternate Key by choosing the + New Key button

![New Alternate Key](https://futurezconsulting.com/wp-content/uploads/2018/10/New-Alternate-Key.png) 

In the dropdown provided, you can select an Entity from the list.  Once the Entity is selected, you can choose from an existing publisher prefix and provide the schema name, display name, and select one or more fields for the key definition.  Once saved, the list of alternate keys will be reloaded.
