import { 
    CREATE_SAFETY_DECLARATION_FAIL,
    CREATE_SAFETY_DECLARATION_REQUEST,
    CREATE_SAFETY_DECLARATION_RESET,
    CREATE_SAFETY_DECLARATION_SUCCESS,
    SAFETY_DECLARATION_DELETE_FAIL,
    SAFETY_DECLARATION_DELETE_REQUEST,
    SAFETY_DECLARATION_DELETE_RESET,
    SAFETY_DECLARATION_DELETE_SUCCESS,
    SAFETY_DECLARATION_DETAIL_FAIL,
    SAFETY_DECLARATION_DETAIL_REQUEST,
    SAFETY_DECLARATION_DETAIL_RESET,
    SAFETY_DECLARATION_DETAIL_SUCCESS,
    SAFETY_DECLARATION_LIST_FAIL, 
    SAFETY_DECLARATION_LIST_REQUEST, 
    SAFETY_DECLARATION_LIST_RESET, 
    SAFETY_DECLARATION_LIST_SUCCESS, 
    UPDATE_SAFETY_DECLARATION_FAIL, 
    UPDATE_SAFETY_DECLARATION_REQUEST, 
    UPDATE_SAFETY_DECLARATION_RESET,
    UPDATE_SAFETY_DECLARATION_SUCCESS
} from "../contsants/safetyDeclarationConstants";


export const getSafetyDeclarationListReducer = (state = {}, action) => {
    switch (action.type) {
        case SAFETY_DECLARATION_LIST_REQUEST:
            return {
                loading: true
            }
        case SAFETY_DECLARATION_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                safetyDeclarationList: action.payload 
            };
        case SAFETY_DECLARATION_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SAFETY_DECLARATION_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const createSafetyDeclarationReducer = (state = {}, action) => {
    switch (action.type) {
        case CREATE_SAFETY_DECLARATION_REQUEST:
            return {
                loading: true
            }
        case CREATE_SAFETY_DECLARATION_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case CREATE_SAFETY_DECLARATION_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case CREATE_SAFETY_DECLARATION_RESET:
            return {}
        default:
            return state;
    }
}

export const updateSafetyDeclarationReducer = (state = {}, action) => {
    switch (action.type) {
        case UPDATE_SAFETY_DECLARATION_REQUEST:
            return {
                loading: true
            }
        case UPDATE_SAFETY_DECLARATION_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case UPDATE_SAFETY_DECLARATION_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case UPDATE_SAFETY_DECLARATION_RESET:
            return {}
        default:
            return state;
    }
}

export const safetyDeclarationDetailReducer = (state = {}, action) => {
    switch (action.type) {
        case SAFETY_DECLARATION_DETAIL_REQUEST:
            return {
                loading: true
            }
        case SAFETY_DECLARATION_DETAIL_SUCCESS:
            return {
                loading: false,
                success: true,
                safetyDetails: action.payload.safetyDetails
            };
        case SAFETY_DECLARATION_DETAIL_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SAFETY_DECLARATION_DETAIL_RESET:
            return {}
        default:
            return state;
    }
}

export const safetyDeclarationDeleteReducer = (state = {}, action) => {
    switch (action.type) {
        case SAFETY_DECLARATION_DELETE_REQUEST:
            return {
                loading: true
            }
        case SAFETY_DECLARATION_DELETE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case SAFETY_DECLARATION_DELETE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case SAFETY_DECLARATION_DELETE_RESET:
            return {}
        default:
            return state;
    }
}