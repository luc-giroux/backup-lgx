sp_helprolemember 'PAF_COST_CONTROL'
sp_addrolemember 'PAF_FINANCE', 'PAF_INVOICE_EDITION_APPLICATION'
sp_addrolemember 'PAF_HR', 'PAF_EMPLOYEE_FORM_APPLICATION'

sp_addrolemember 'PAF_FINANCE', 'PAF_COST_ADJUSTMENTS_APPLICATION'
sp_addrolemember 'PAF_TIMESHEET_ENTRY', 'PAF_TIMESHEET_LOAD_APPLICATION'

+ local policy: com activation to NETWORK SERVICE

+ sql reporting services: all reports available to NETWORK SERVICE