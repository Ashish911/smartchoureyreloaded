import { CREATE_DISASTER_FAIL, CREATE_DISASTER_REQUEST, CREATE_DISASTER_RESET, CREATE_DISASTER_SUCCESS, DISASTER_DELETE_FAIL, DISASTER_DELETE_REQUEST, DISASTER_DELETE_RESET, DISASTER_DELETE_SUCCESS, DISASTER_DETAIL_FAIL, DISASTER_DETAIL_REQUEST, DISASTER_DETAIL_SUCCESS, GET_DISASTER_LIST_FAIL, GET_DISASTER_LIST_REQUEST, GET_DISASTER_LIST_RESET, GET_DISASTER_LIST_SUCCESS, MULTIPLE_DISASTER_DELETE_FAIL, MULTIPLE_DISASTER_DELETE_REQUEST, MULTIPLE_DISASTER_DELETE_RESET, MULTIPLE_DISASTER_DELETE_SUCCESS, UPDATE_DISASTER_FAIL, UPDATE_DISASTER_REQUEST, UPDATE_DISASTER_RESET, UPDATE_DISASTER_SUCCESS } from "../contsants/disasterConstants";

export const getDisasterListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_DISASTER_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_DISASTER_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_DISASTER_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_DISASTER_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const disasterDetailReducer = (state = {}, action) => {
    switch (action.type) {
        case DISASTER_DETAIL_REQUEST:
            return {
                loading: true
            }
        case DISASTER_DETAIL_SUCCESS:
            return {
                loading: false,
                success: true,
                disasterInfo: action.payload
            };
        case DISASTER_DETAIL_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const disasterDeleteReducer = (state = {}, action) => {
    switch (action.type) {
        case DISASTER_DELETE_REQUEST:
            return {
                loading: true
            }
        case DISASTER_DELETE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case DISASTER_DELETE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case DISASTER_DELETE_RESET:
            return {}
        default:
            return state;
    }
}

export const createDisasterReducer = (state = {}, action) => {
    switch (action.type) {
        case CREATE_DISASTER_REQUEST:
            return {
                loading: true
            }
        case CREATE_DISASTER_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case CREATE_DISASTER_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case CREATE_DISASTER_RESET:
            return {}
        default:
            return state;
    }
}

export const disasterUpdateReducer = (state = {}, action) => {
    switch (action.type) {
        case UPDATE_DISASTER_REQUEST:
            return {
                loading: true
            }
        case UPDATE_DISASTER_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case UPDATE_DISASTER_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case UPDATE_DISASTER_RESET:
            return {}
        default:
            return state;
    }
}

export const multipleDisasterDeleteReducer = (state = {}, action) => {
    switch (action.type) {
        case MULTIPLE_DISASTER_DELETE_REQUEST:
            return {
                loading: true
            }
        case MULTIPLE_DISASTER_DELETE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case MULTIPLE_DISASTER_DELETE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case MULTIPLE_DISASTER_DELETE_RESET:
            return {}
        default:
            return state;
    }
}