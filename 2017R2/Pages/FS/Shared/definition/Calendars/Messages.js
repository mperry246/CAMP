//Messsages Definition

var Messages = {
    loadingText: 'Searching...',

    /*Rigth Click Menu Text*/
    filterResource: 'Filter Staff',

    /*Button Messages*/
    buttonTextSave: 'Save',
    buttonTextCancel: 'Cancel',
    buttonTextDelete: 'Delete',
    buttonTextFilter: 'Filter',
    buttonTextClear: 'Clear',
    buttonTextClose: 'Close',
    buttonTextFilterClose: 'Filter & Close',

    /*Empty Field Messages*/
    emptyTextServieOrder: 'Select a Service Order',
    emptyTextEmployee: 'Select a Staff',
    emptyTextBranchLocation: 'Select a Branch Location',
    emptyTextBranch: 'Select a Branch',
    emptyTextHours: 'Hours',
    employeeNotFound: 'Staff not found',
    emptyTextEmployeeNameFilter: 'Type Name',

    /*Alert Messages*/
    alertNoAppointment: 'There are no Service Orders available for Appointments.',
    alertDeleteAppointment: 'Are you sure you want to delete this Appointment?',
    alertDeleteTimeSlot: 'Are you sure you want to delete this TimeSlot?',
    alertDeleteShared: 'This appointment is shared for more than one user. Are you sure you want to remove it?',
    alertSlaTimeExceeded: 'The SLA assigned to this Service Order has already expired.',
    alertSetupNotDefined: 'It seems that the Calendar Board preferences have not been set. Please go to the Service Management Preferences and check the Calendar Board options.',

    /*Error Messages*/
    errorAjaxFailure: 'Error connecting to the server, please try again.',
    errorFieldDuration: 'Duration cannot surpass current day.',
    errorSharedAppointment: 'This Appointment has been already shared with this Staff. A Staff is not allowed to have the same Appointment more than once.',
    errorHexaField: 'Invalid value. The field must be 6 characters long and in hexadecimal notation.',
    errorRGBNumber: 'Invalid value. The field must be between 0 and 255.',

    /*Tooltip Messages*/
    phoneNumberMissing: 'Phone number missing.',
    warningAppointmentValidatedByDispatcher: 'This Appointment is not validated by dispatcher',

    /*Note Messages*/
    noteGeozoneFilterText: 'This specific tab filters by any options selected.',

    warningAppointmentValid: function(employeeName) {
        if (employeeName != null) {
            return 'There is more than one ' + Titles.appointment.toLowerCase() + ' scheduled at the same time for ' + employeeName + '.';
        } else {
            return 'There is more than one ' + Titles.appointment.toLowerCase() + ' scheduled at the same time.';
        }
    },
    warningResourceAvailable: function(employeeName) {
        if (employeeName != null) {
            return 'This ' + Titles.appointment.toLowerCase() + ' is scheduled out of the working time for ' + employeeName + '.';
        } else {
            return 'This ' + Titles.appointment.toLowerCase() + ' is scheduled out of the working time.';
        }
    },
    totalEmployeesShared: function(employeeNumber) {
        return 'This ' + Titles.appointment.toLowerCase() + ' is shared with ' + employeeNumber + ' ' + Titles.employees.toLowerCase() + '. To see the shared ' + Titles.employees.toLowerCase() + ' click on me.'
    }
};