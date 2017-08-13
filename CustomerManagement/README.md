# CustomerManagement
An exercise given to me, basic get and upsert. Featuring NUnit, Specflow (with in-memory database tests) and Rhinomocks.

#### Restoring
> `Update-Package -reinstall`

If you have any issues with the NuGet restore run `Update-Package -reinstall` from the Package Manager Console to re-path the hintpaths in the .csproj files.

The scripts from the database project will also be xcopied to `C:\Temp\CustomerManagement.Database\Scripts`. This is so that ncrunch can pick them up when running the in-memory tests.

## Criteria

X Company require a web based user interface to manage prospective and existing customer records, containing information on particular characteristics and preferences captured from various sources. The solution must be quick, responsive, and reliable. 

This is an application for internal use and as such there are no specific design requirements, the client’s priority is that it is functional.

### Deliverables
*	The database should be seeded with an existing dataset, which will be provided in CSV format
*	The following screens are required
*	Dashboard containing list of customers
*	Update form
*	Insert form

#### Dashboard
The default screen will contain a table containing the latest 20 customers ordered by most recent. The table should contain the following columns

| Title | Display Type | Description |
|-------|--------------|-------------|
| Name | Hyperlink | - The customers full name - Will link to the update form |
| Previously Ordered | Yes / No | Value displayed in green if yes, red if no |
| Web Customer    |    Yes / No    | Value displayed in green if yes, red if no |
| Date Active     |    dd/mm/yyyy  | The date the customer was last active |
| Is Palindrome   |    Yes / No    | - Value displayed in green if yes, red if no. - Yes or No depending on whether the full name is spelt the same forwards as it is backwards. - The match should not take spaces into account and should also be case insensitive. |
| Favourite Colours | Comma separated list | |

The dashboard will also require a link to insert new records

#### Update Form
The update form will contain a set of fields to update the database record for the selected customer containing the following populated fields.

| Form Label    | Field Type    | Validation    |
|-----------------------|-----------------------------------------------------|-----------------------------------------------------------------|
| First Name    | Text input    | Required    |
| Previously Ordered    | Checkbox    | None    |
| Web Customer    | Checkbox    | None    |
| Date Active    | Text input    | - Required - Date format validation in the format dd/mm/yyyy    |
| Colours    | List of checkbox   options: - Red - Green - Blue    | None    |

Once updated the update screen will be closed and user will be taken back to the dashboard.

#### Insert Form

The update form will contain a set of fields to update the database record for the selected customer containing the following populated fields.

| Form Label    | Field Type    | Validation    |
|-----------------------|-----------------------------------------------------|-----------------------------------------------------------------|
| First Name    | Text input    | Required    |
| Previously Ordered    | Checkbox    | None    |
| Web Customer    | Checkbox    | None    |
| Date Active    | Text input    | - Required - Date format validation in the format dd/mm/yyyy    |
| Colours    | List of checkbox   options: - Red - Green - Blue    | None    |

Once inserted the insert screen will be closed and user will be taken back to the dashboard.