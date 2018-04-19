//Titles Definition

var Titles = {
   /*General Titles*/

   dashboard: 'Dashboard',
   appointment: 'Appointment',
   newAppointment: 'New Appointment',
   appointmentInquiry: 'Appointment Inquiry',
   serviceOrder: 'Service Orders',
   unassignedAppointment: 'Unassigned Appointments',
   editAppointment: 'Edit Appointment',
   view: 'View',
   edit: 'Edit',
   cloneAppointment: 'Clone',
   shareAppointment: 'Share',
   validateByDispatch: 'Validate by Dispatcher',
   clearValidation: 'Clear Validation',
   confirmAppointment: 'Confirm',
   unconfirmAppointment: 'Unconfirm',
   appointmentInfo: 'Appointment Info',
   resourceFilters: 'Staff Filters:',
   problems: 'Problems',
   skills: 'Skills',
   servicesType: 'Service Classes',
   assignedEmployee: 'Assigned Staff',
   services: 'Services',
   geoZones: 'Service Areas',
   employee: 'Staff',
   employees: 'Staff',
   licensesType: 'Licenses Type',
   employeesFilters: 'Staff Filters',
   error: 'Error',
   warning: 'Warning',
   deleteTitle: 'Delete',
   alert: 'Alert',
   serviceOrderFilters: 'Service Order Filters',
   unassignedAppointmentFilters: 'Unassigned Appointment Filters',
   userCalendars: 'Staff Calendar',
   branchLocation: 'Branch Location',
   branch: 'Branch',
   location: 'Location',
   configuration: 'Configuration',
   serviceOrderType: 'Service Order Type',
   employeeNameFilter: 'Name Filter',

   /*Service Order Grid Header*/
   refNbr: 'Service Order',
   customer: 'Customer',
   orderDate: 'Date',
   salesOrder: 'Sales Order',
   cases: 'Case',
   stage: 'Stage',
   priority: 'Priority',
   severity: 'Severity',
   assignedEmployeeID: 'Assigned Staff ID',
   sLA: 'SLA',
   servicesRemaning: 'Service Remaning',
   servicesCount: 'Quantity',
   sourceType: 'Source Type',
   estimatedTime: 'Estimated Duration',
   sourceRef: 'Source Ref.',
   assignedEmployee: 'Supervisor',
   timeExceeded: 'Time Exceeded',
   date: 'Date',
   timeStart: 'Start Time',
   timeEnd: 'End Time',

   /*Calendar Preferences*/
   appointmentPreferences: 'Appointment Preferences',
   inactivePreferences: 'Inactive Fields',
   activePreferences: 'Active Fields',
   previewPreferences: 'Preview',
   statusPreferences: 'Display Preferences'
};

// Valid Appointment params
var AppointmentPageParams = [
   'RefNbr',
   'SrvOrdType',
   'SORefNbr',
   'RoomID',
   'ScheduledDateTimeBegin',
   'ScheduledDateTimeEnd'
];

// Appointment Status Name
var AppointmentStatusName = {
   confirmed: 'confirmed',
   validated: 'validated',
   scheduled: 'scheduled',
   canceled: 'canceled',
   completed: 'completed',
   inprocess: 'in process',
   closed: 'closed',
   onhold: 'on hold'
};