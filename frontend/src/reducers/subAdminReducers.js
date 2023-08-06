import { 
    SUB_ADMIN_LIST_REQUEST, 
    SUB_ADMIN_LIST_SUCCESS, 
    SUB_ADMIN_LIST_FAIL, 
    SUB_ADMIN_LIST_RESET, 
    SUB_ADMIN_ASSIGN_REQUEST, 
    SUB_ADMIN_ASSIGN_SUCCESS, 
    SUB_ADMIN_ASSIGN_FAIL, 
    SUB_ADMIN_ASSIGN_RESET, 
    SUB_ADMIN_DELETE_RESET,
    SUB_ADMIN_DELETE_FAIL,
    SUB_ADMIN_DELETE_SUCCESS,
    SUB_ADMIN_DELETE_REQUEST
} from "../contsants/subAdminConstants";

export const getSubAdminListReducer = (state = {}, action) => {
    switch (action.type) {
        case SUB_ADMIN_LIST_REQUEST:
            return {
                loading: true
            }
        case SUB_ADMIN_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case SUB_ADMIN_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SUB_ADMIN_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const assignSubAdminReducer = (state = {}, action) => {
    switch (action.type) {
        case SUB_ADMIN_ASSIGN_REQUEST:
            return {
                loading: true
            }
        case SUB_ADMIN_ASSIGN_SUCCESS:
            return {
                loading: false,
                success: true,
                menuList: action.payload 
            };
        case SUB_ADMIN_ASSIGN_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SUB_ADMIN_ASSIGN_RESET:
            return {}
        default:
            return state;
    }
}

export const subAdminDeleteReducer = (state = {}, action) => {
    switch (action.type) {
        case SUB_ADMIN_DELETE_REQUEST:
            return {
                loading: true
            }
        case SUB_ADMIN_DELETE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case SUB_ADMIN_DELETE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SUB_ADMIN_DELETE_RESET:
            return {}
        default:
            return state;
    }
}