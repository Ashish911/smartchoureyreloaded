import {
    SITE_CODE_CREATE_REQUEST,
    SITE_CODE_CREATE_SUCCESS,
    SITE_CODE_CREATE_FAIL,
    SITE_CODE_CREATE_RESET,
    SITE_ASSIGNED_LIST_REQUEST,
    SITE_ASSIGNED_LIST_SUCCESS,
    SITE_ASSIGNED_LIST_FAIL,
    SITE_UNASSIGNED_LIST_REQUEST,
    SITE_UNASSIGNED_LIST_SUCCESS,
    SITE_UNASSIGNED_LIST_FAIL,
    SITE_LIST_REQUEST,
    SITE_LIST_SUCCESS,
    SITE_LIST_FAIL,
    SITE_CODE_VALIDATION_REQUEST,
    SITE_CODE_VALIDATION_SUCCESS,
    SITE_CODE_VALIDATION_FAIL,
    SITE_CODE_DELETE_REQUEST,
    SITE_CODE_DELETE_SUCCESS,
    SITE_CODE_DELETE_FAIL,
    SITE_INFORMATION_CREATE_REQUEST,
    SITE_INFORMATION_CREATE_SUCCESS,
    SITE_INFORMATION_CREATE_FAIL,
    SITE_INFORMATION_DETAIL_REQUEST,
    SITE_INFORMATION_DETAIL_SUCCESS,
    SITE_INFORMATION_DETAIL_FAIL,
    SITE_ALL_LIST_SUCCESS,
    SITE_ALL_LIST_REQUEST,
    SITE_ALL_LIST_FAIL,
    ALL_SITES_SPACE_LIST_FAIL,
    ALL_SITES_SPACE_LIST_SUCCESS,
    ALL_SITES_SPACE_LIST_REQUEST,
    SITE_SPACE_DETAIL_SUCCESS,
    SITE_SPACE_DETAIL_REQUEST,
    SITE_SPACE_DETAIL_FAIL,
    SITE_INFORMATION_CREATE_RESET,
    SITE_INFORMATION_UPDATE_REQUEST,
    SITE_INFORMATION_UPDATE_SUCCESS,
    SITE_INFORMATION_UPDATE_FAIL,
    SITE_INFORMATION_UPDATE_RESET,
    SITE_DETAIL_BY_QR_REQUEST,
    SITE_DETAIL_BY_QR_SUCCESS,
    SITE_DETAIL_BY_QR_FAIL,
    SITE_DETAIL_BY_QR_RESET,
    ASSIGN_SITE_SPACE_REQUEST,
    ASSIGN_SITE_SPACE_SUCCESS,
    ASSIGN_SITE_SPACE_FAIL,
    ASSIGN_SITE_SPACE_RESET,
    SITE_CODE_DELETE_RESET
} from '../contsants/siteConstants'

export const siteCodeCreateReducer = (state = {}, action) => {
    switch (action.type) {
        case SITE_CODE_CREATE_REQUEST:
            return {
                loading: true
            }
        case SITE_CODE_CREATE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case SITE_CODE_CREATE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SITE_CODE_CREATE_RESET:
            return {}
        default:
            return state;
    }
}

export const allSiteCodeListReducer = (state = { assigned: [], unassigned: [] }, action) => {
    switch (action.type) {
        case SITE_ALL_LIST_REQUEST:
            return {
                loading: true
            }
        case SITE_ALL_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                assigned: action.payload.assigned,
                unassigned: action.payload.unassigned
            };
        case SITE_ALL_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const unassigendSiteCodeListReducer = (state = { unAssignedCodes: [] }, action) => {
    switch (action.type) {
        case SITE_UNASSIGNED_LIST_REQUEST:
            return {
                loading: true
            }
        case SITE_UNASSIGNED_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                unAssignedCodes: action.payload.unAssignedCodes
            };
        case SITE_UNASSIGNED_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const deleteSiteCodeReducer = (state = {}, action) => {
    switch (action.type) {
        case SITE_CODE_DELETE_REQUEST:
            return {
                loading: true
            }
        case SITE_CODE_DELETE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case SITE_CODE_DELETE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SITE_CODE_DELETE_RESET:
            return {}
        default:
            return state;
    }
}

export const listSiteReducer = (state = { siteList: [] }, action) => {
    switch (action.type) {
        case SITE_LIST_REQUEST:
            return {
                loading: true
            }
        case SITE_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                siteList: action.payload
            };
        case SITE_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const siteCodeValidateReducer = (state = {}, action) => {
    switch (action.type) {
        case SITE_CODE_VALIDATION_REQUEST:
            return {
                loading: true
            }
        case SITE_CODE_VALIDATION_SUCCESS:
            return {
                loading: false,
                validated: action.payload.isSuccess,
                siteCode: action.payload.siteCode
            };
        case SITE_CODE_VALIDATION_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const createSiteReducer = (state = {}, action) => {
    switch (action.type) {
        case SITE_INFORMATION_CREATE_REQUEST:
            return {
                loading: true
            }
        case SITE_INFORMATION_CREATE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case SITE_INFORMATION_CREATE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SITE_INFORMATION_CREATE_RESET:
            return {}
        default:
            return state;
    }
}

export const updateSiteReducer = (state = {}, action) => {
    switch (action.type) {
        case SITE_INFORMATION_UPDATE_REQUEST:
            return {
                loading: true
            }
        case SITE_INFORMATION_UPDATE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case SITE_INFORMATION_UPDATE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SITE_INFORMATION_UPDATE_RESET:
            return {}
        default:
            return state;
    }
}

export const siteDetailReducer = (state = {}, action) => {
    switch (action.type) {
        case SITE_INFORMATION_DETAIL_REQUEST:
            return {
                loading: true
            }
        case SITE_INFORMATION_DETAIL_SUCCESS:
            return {
                loading: false,
                success: true,
                siteInfo: action.payload
            };
        case SITE_INFORMATION_DETAIL_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const allSitesSpaceReducer = (state = {}, action) => {
    switch (action.type) {
        case ALL_SITES_SPACE_LIST_REQUEST:
            return {
                loading: true
            }
        case ALL_SITES_SPACE_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                siteSpace: action.payload
            };
        case ALL_SITES_SPACE_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const getSiteSpaceReducer = (state = {}, action) => {
    switch (action.type) {
        case SITE_SPACE_DETAIL_REQUEST:
            return {
                loading: true
            }
        case SITE_SPACE_DETAIL_SUCCESS:
            return {
                loading: false,
                success: true,
                detail: action.payload
            };
        case SITE_SPACE_DETAIL_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const assignSiteSpaceReducer = (state = {}, action) => {
    switch (action.type) {
        case ASSIGN_SITE_SPACE_REQUEST:
            return {
                loading: true
            }
        case ASSIGN_SITE_SPACE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case ASSIGN_SITE_SPACE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case ASSIGN_SITE_SPACE_RESET:
            return {};
        default:
            return state;
    }
}

export const getSiteByQrReducer = (state = {}, action) => {
    switch (action.type) {
        case SITE_DETAIL_BY_QR_REQUEST:
            return {
                loading: true
            }
        case SITE_DETAIL_BY_QR_SUCCESS:
            return {
                loading: false,
                success: true,
                detail: action.payload
            };
        case SITE_DETAIL_BY_QR_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SITE_DETAIL_BY_QR_RESET:
            return {}
        default:
            return state;
    }
}