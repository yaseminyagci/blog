


const helper = {
    getNestedObject: function(nestedObj, pathArr) {
        return pathArr.reduce((obj, key) =>
            (obj && obj[key] !== 'undefined') ? obj[key] : undefined,
            nestedObj);
    },
    groupBy: function(arr, keys) {
        return arr.reduce(function(rv, x) {
                (rv[helper.getNestedObject(x, keys)] = rv[helper.getNestedObject(x, keys)] || []).push(x);
                return rv;
            },
            {});
    },
    //groupBy: function(arr, keys) {
    //    return arr.reduce(function(rv, x) {
    //            (rv[x[keys]] = rv[x[keys]] || []).push(x);
    //            return rv;
    //        },
    //        {});
    //},
    findObject: function(arr, propertys, data) {

        return arr.find(x => helper.getNestedObject(x, propertys) === data);

    },
    findMin: function(arr, propertys) {
        let min = helper.getNestedObject(arr[0], propertys);
        if (typeof min === 'undefined' || min === null)
            return null;

        for (let i = 1, len = arr.length; i < len; i++) {
            let v = helper.getNestedObject(arr[i], propertys);
            min = (v < min) ? v : min;
        }

        return min;
    },
    findMax: function(arr, propertys) {
        let max = arr[0];
        if (typeof max === 'undefined' || max === null)
            return null;

        for (let i = 1, len = arr.length; i < len; i++) {
            let v = arr[i];
            max = (helper.getNestedObject(v, propertys) > helper.getNestedObject(max, propertys)) ? v : max;
        }

        return max;
    },

    arraysEqual: function(a, b) {
        if (a === b) return true;
        if (a == null || b == null) return false;
        if (a.length !== b.length) return false;

        // If you don't care about the order of the elements inside
        // the array, you should sort both arrays here.
        // Please note that calling sort on an array will modify that array.
        // you might want to clone your array first.

        for (var i = 0; i < a.length; ++i) {
            if (a[i] == null || b[i] == null) continue;
            if (a[i] !== b[i]) return false;
        }

        return true;
    },
    dynamicSort: function(property, isDescending) {
        var sortOrder = 1;
        if (isDescending) {
            sortOrder = -1;

        }
        return function(a, b) {
            /* next line works with strings and numbers, 
             * and you may want to customize it to your needs
             */
            var aProp = helper.getNestedObject(a, property);
            var bProp = helper.getNestedObject(b, property);
            var result = (aProp < bProp) ? -1 : (aProp > bProp) ? 1 : 0;
            return result * sortOrder;
        }
    },
    arrayContains: function(arr, val) {
        return $.inArray(val, arr) !== -1
    },
    stringIncludeInArray(s, arr) {

        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];
            if (s.includes(item))
                return true;

        }
        return false;
    },
    objectToFormData: function(object) {
        const formData = new FormData();
        Object.keys(object).forEach(key => formData.append(key, object[key]));
        return formData;
    },
    capitalizeString: function(str) {

        str = str.toLowerCase().replace(/\b[a-z]/g,
            function(letter) {
                return letter.toUpperCase();
            });

        return str;
    },


    setModalHeader: function(modal, text) {
        let title = $(modal).find(".modal-header .modal-title");
        $(title).text(text);
    },
    resetForm(form) {
        $(form).trigger('reset');
    },
    buildOptionsHtmlString: function(arr) {
        var html = "";
        for (var i = 0; i < arr.length; i++) {
            html += `<option value="${arr[i].value}">${arr[i].text}</option>`;
        }
        return html;
    },
    renderTableStatusContent: function(status) {

        let arr = {
            1: { 'title': 'Aktif', 'class': ' kt-badge--success' },
            2: { 'title': 'Pasif', 'class': 'kt-badge--brand' },
            4: { 'title': 'Arızalı', 'class': ' kt-badge--danger' },
        };
        let object = arr[status];

        if (typeof object === 'undefined') {
            object = arr[1];
        }
        return '<span class="kt-badge ' +
            object.class +
            ' kt-badge--inline kt-badge--pill">' +
            object.title +
            '</span>';

    },
    setContentVisibility: function(content, status) {
        if (status)
            $(content).show();
        else
            $(content).hide();
    },
    minuteToHour(min) {
        var hours = Math.floor(min / 60);
        var minutes = min % 60;
        return {
             hours,
             minutes
        }
    },
    distinctArray(arr) {
        let onlyUnique = function(value, index, self) {
            return self.indexOf(value) === index;
        };
        return  arr.filter( onlyUnique );
    }
};

