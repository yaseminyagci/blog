const selector = {
    getComments: (id) => {
        let data = null;
        request.Get("/api/comment/GetByPost/" + id,
            null,
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    },
    getTags: () => {
        let data = null;
        request.Get("/api/tags/getall",
            null,
            function (response) {             
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    },
    getSelectedTags: (id) => {
        let data = null;
        request.Get("/api/post/GetTag/"+id,
            null,
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    },
    getCategories: () => {
        let data = null;
        request.Get(URLS.CATEGORY.GET_ALL,
            null,
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, {async:false});
        return data;
    },
    getVehicleTypes: () => {
        let data = null;
        request.Get(URLS.VEHICLE_TYPE.GET_ALL,
            null,
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    },
    getDrivers: () => {
        let data = null;
        request.Get(URLS.DRIVER.GET_ALL,
            null,
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    },
    getVehicles: (vehicleTypes) => {
        let data = null;
        request.Get(URLS.VEHICLE.GET_ALL,
            { vehicleTypes },
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    },
    getVehicleByVehicleTypes: (vehicleTypes) => {
        let data = null;
        request.Get(URLS.VEHICLE.GET_BY_VEHICLE_TYPES,
            { vehicleTypes },
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    },
    getShifts: () => {
        let data = null;
        request.Get(URLS.SHIFT.GET_ALL,
            null,
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    },   
    getRoles: () => {
        let data = null;
        request.Get(URLS.ROLE.GET_ALL,
            null,
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    }, 
    getButtonSettings: () => {
        let data = null;
        request.Get(URLS.BUTTON_SETTINGS.GET_ALL,
            null,
            function (response) {
                if (!response.isError)
                    data = response.data;
            }, { async: false });
        return data;
    }
}