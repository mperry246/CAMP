//Titles Definition

var TX = {
    /*** Titles ***/
    titles: {
        /* Employee Routes*/
        appointment: 'Appointment',
        cancelChanges: 'Cancel changes',
        employeeList: 'Staff List',
        treeviewTitle: 'Resources',
        postalCode: 'Postal Code',
        location: 'Location',
        travelTime: 'Travel Time',
        address: 'Address',
        /*Routes*/
        customer: 'Route/Customer',
        serviceType: 'Service Type',
        duration: 'Duration',
        servicesDuration: 'Services Duration',
        routeList: 'Routes List',
        routeTreeTitle: 'Routes',
        employeeTreeTitle: 'Staff',
        routeInfoTitle: 'Route Information',
        appointmentInfoTitle: 'Appointments Information'
    },

    /*Context menu*/
    appointmentInfo: 'Appointment Info',
    routeInfo: 'Route Info',
    removeNode: 'Remove Resource',
    showRouteInfoWindow: 'Show/Hide Route\'s Appointment Information on Map',
    showRouteDevice: 'Show/Hide GPS location on Map',
    collapseAll: 'Collapse All',

    /*Context menu tooltip*/
    appointmentInfoToolTip: 'Open appointment information',
    resourceInfoToolTip: 'Open resource information',
    showRouteInfoWindowToolTip: 'Show/Hide all information tooltips of this route on map.',
    removeNodeToolTip: 'Remove this resource from list',
    expandCollapseRouteToolTip: 'Expand/Collapse all appointments of this route',
    collapseAllToolTip: 'Collapse all nodes',
    showRouteDeviceToolTip: 'Show/Hide GPS location on Map',

    /* Messages */
    messages: {
        discardUnsavedChanges: 'Any unsaved will be discarded',
        unassignedContact: 'Unassigned appointments',
        googleRouteError: 'There is an error with the appointment of this route. Please verify the address.',
        calculate: '(Calculate)',
        mapApiError: 'This route cannot be traced with the provided data. Please revise the appointments addresses, some are not recognized by the system.'
    },

    /* Buttons */
    buttons: {
        accept: 'Accept',
        cancel: 'Cancel',
        calculate: 'Calculate',
    },

    /* Gmap Marlker title prefix*/
    //@TODO SD-3557 get this from the setup
    markerTitlePrefix: 'Appointment Ref:'

}