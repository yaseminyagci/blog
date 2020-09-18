//const SERVER_URL = '';
//const LOCAL_URL = 'https://localhost:5001';

const protocol = location.protocol;
const host = protocol + "//" + location.host;
const URLS = {
    HOST: host,
    POST: {
        INDEX: '/post',
        GET_ALL: '/api/post/getall',
        //GET_BY_VEHICLE_TYPES: '/api/posts/getbyvehicletypes',
        GET: '/api/post/get',
        ADD: '/api/post/add',
        EDIT: '/api/post/edit',
        DELETE: '/api/post/delete',
        ADDPAGE: '/postadd',
        EDITPAGE: '/postedit/',
        DETAILPAGE: '/postdetail/',
        
    },
    USERS: {
        INDEX: '/index',
        GET_ALL: '/api/user/getall',
        //GET_BY_VEHICLE_TYPES: '/api/posts/getbyvehicletypes',
        GET: '/api/user/get',
        ADD: '/api/user/add',
        EDIT: '/api/user/edit',
        DELETE: '/api/user/delete',
        ADDPAGE: '/useradd',
        EDITPAGE: '/useredit/',
        DETAILPAGE: '/userdetail/',
    },
    TAGS: {
        INDEX: '/index',
        GET_ALL: '/api/tags/getall',
        //GET_BY_VEHICLE_TYPES: '/api/posts/getbyvehicletypes',
        GET: '/api/tags/get',
        ADD: '/api/tags/add',
        EDIT: '/api/tags/edit',
        DELETE: '/api/tags/delete',
    },
    LIKES: {
        GET_ALL: '/api/Likes/getall',
        //GET_BY_VEHICLE_TYPES: '/api/posts/getbyvehicletypes',
        GET: '/api/Likes/get',
        GETBYPOST: '/api/Likes/getByPost/',
        ADD: '/api/Likes/add',
        EDIT: '/api/Likes/edit',
        DELETE: '/api/Likes/delete',
    },
    COMMENT: {
        GET_ALL: '/api/Comment/getall',
        //GET_BY_VEHICLE_TYPES: '/api/posts/getbyvehicletypes',
        GET: '/api/Comment/get',
        ADD: '/api/Comment/add',
        EDIT: '/api/Comment/edit',
        DELETE: '/api/Comment/delete',
    },
    ACCOUNT: {
        LOGIN: 'Login',
        LOGOUT: '/api/account/logout',
    },
    HOME: {
        INDEX: 'Dashboard/',
        GUEST: 'Index/',
        LOGIN: 'login/'
    },
    ROLE: {
        INDEX: '/Roles',
        GET_ALL: '/api/role/getall',
        ADD: '/api/role/add',
        EDIT_ROUTE: '/EditRole',
        DELETE: '/api/role/delete',

    },
    VEHICLE: {
        INDEX: '/Vehicles',
        GET_ALL: '/api/vehicle/getall',
        GET_BY_VEHICLE_TYPES: '/api/vehicle/getbyvehicletypes',
        GET: '/api/vehicle/get',
        ADD: '/api/vehicle/add',
        EDIT: '/api/vehicle/edit',
        DELETE: '/api/vehicle/delete',
        MAINTENANCE: '/api/vehicle/setmaintenance',
        UNMAINTENANCE: '/api/vehicle/setunmaintenance',
    }
}


function redirect(URL) {
    location.href = "/" + URL;
}
