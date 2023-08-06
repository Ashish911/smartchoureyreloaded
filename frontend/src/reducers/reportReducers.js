import { GET_ADMIN_REPORT_LIST_FAIL, GET_ADMIN_REPORT_LIST_REQUEST, GET_ADMIN_REPORT_LIST_RESET, GET_ADMIN_REPORT_LIST_SUCCESS, GET_DEVICE_REPORT_LIST_FAIL, GET_DEVICE_REPORT_LIST_REQUEST, GET_DEVICE_REPORT_LIST_RESET, GET_DEVICE_REPORT_LIST_SUCCESS, GET_LOGGED_USER_REPORT_LIST_FAIL, GET_LOGGED_USER_REPORT_LIST_REQUEST, GET_LOGGED_USER_REPORT_LIST_RESET, GET_LOGGED_USER_REPORT_LIST_SUCCESS, GET_OPERATION_REPORT_LIST_FAIL, GET_OPERATION_REPORT_LIST_REQUEST, GET_OPERATION_REPORT_LIST_RESET, GET_OPERATION_REPORT_LIST_SUCCESS, GET_SAFETY_DECLARATION_REPORT_LIST_FAIL, GET_SAFETY_DECLARATION_REPORT_LIST_REQUEST, GET_SAFETY_DECLARATION_REPORT_LIST_RESET, GET_SAFETY_DECLARATION_REPORT_LIST_SUCCESS, GET_SITE_USER_SMARTPHONE_REPORT_LIST_FAIL, GET_SITE_USER_SMARTPHONE_REPORT_LIST_REQUEST, GET_SITE_USER_SMARTPHONE_REPORT_LIST_RESET, GET_SITE_USER_SMARTPHONE_REPORT_LIST_SUCCESS, GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_FAIL, GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_REQUEST, GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_RESET, GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_SUCCESS, GET_SP_SAFETY_DECLARATION_REPORT_LIST_FAIL, GET_SP_SAFETY_DECLARATION_REPORT_LIST_REQUEST, GET_SP_SAFETY_DECLARATION_REPORT_LIST_RESET, GET_SP_SAFETY_DECLARATION_REPORT_LIST_SUCCESS, GET_SUB_ADMIN_REPORT_LIST_FAIL, GET_SUB_ADMIN_REPORT_LIST_REQUEST, GET_SUB_ADMIN_REPORT_LIST_RESET, GET_SUB_ADMIN_REPORT_LIST_SUCCESS, GET_USER_ACCESS_REPORT_LIST_FAIL, GET_USER_ACCESS_REPORT_LIST_REQUEST, GET_USER_ACCESS_REPORT_LIST_RESET, GET_USER_ACCESS_REPORT_LIST_SUCCESS } from "../contsants/reportConstants";

export const getUserReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_USER_ACCESS_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_USER_ACCESS_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_USER_ACCESS_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_USER_ACCESS_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const getSafetyDeclarationReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_SAFETY_DECLARATION_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_SAFETY_DECLARATION_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_SAFETY_DECLARATION_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_SAFETY_DECLARATION_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const getSmartPhoneUserReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const getSpSafetyDeclarationReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_SP_SAFETY_DECLARATION_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_SP_SAFETY_DECLARATION_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_SP_SAFETY_DECLARATION_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_SP_SAFETY_DECLARATION_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const getSiteUserSmartPhoneReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_SITE_USER_SMARTPHONE_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_SITE_USER_SMARTPHONE_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_SITE_USER_SMARTPHONE_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_SITE_USER_SMARTPHONE_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const getOperationReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_OPERATION_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_OPERATION_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_OPERATION_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_OPERATION_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}

// SUPER ADMIN

export const getAdminReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_ADMIN_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_ADMIN_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_ADMIN_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_ADMIN_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const getSubAdminReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_SUB_ADMIN_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_SUB_ADMIN_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_SUB_ADMIN_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_SUB_ADMIN_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const getLoggedUserReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_LOGGED_USER_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_LOGGED_USER_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_LOGGED_USER_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_LOGGED_USER_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const getDeviceReportListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_DEVICE_REPORT_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_DEVICE_REPORT_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_DEVICE_REPORT_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_DEVICE_REPORT_LIST_RESET:
            return {}
        default:
            return state;
    }
}