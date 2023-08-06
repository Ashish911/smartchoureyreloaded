import { 
    CHOUREY_ONE_DELETE_FAIL,
    CHOUREY_ONE_DELETE_REQUEST,
    CHOUREY_ONE_DELETE_RESET,
    CHOUREY_ONE_DELETE_SUCCESS,
    CHOUREY_ONE_DETAIL_FAIL,
    CHOUREY_ONE_DETAIL_REQUEST,
    CHOUREY_ONE_DETAIL_SUCCESS,
    CHOUREY_TWO_DELETE_FAIL,
    CHOUREY_TWO_DELETE_REQUEST,
    CHOUREY_TWO_DELETE_RESET,
    CHOUREY_TWO_DELETE_SUCCESS,
    CHOUREY_TWO_DETAIL_FAIL,
    CHOUREY_TWO_DETAIL_REQUEST,
    CHOUREY_TWO_DETAIL_SUCCESS,
    CREATE_CHOUREY_ONE_FAIL,
    CREATE_CHOUREY_ONE_REQUEST,
    CREATE_CHOUREY_ONE_RESET,
    CREATE_CHOUREY_ONE_SUCCESS,
    CREATE_CHOUREY_TWO_FAIL,
    CREATE_CHOUREY_TWO_REQUEST,
    CREATE_CHOUREY_TWO_RESET,
    CREATE_CHOUREY_TWO_SUCCESS,
    GET_CHOUREY_ONE_LIST_FAIL, 
    GET_CHOUREY_ONE_LIST_REQUEST, 
    GET_CHOUREY_ONE_LIST_RESET, 
    GET_CHOUREY_ONE_LIST_SUCCESS, 
    GET_CHOUREY_TWO_LIST_FAIL, 
    GET_CHOUREY_TWO_LIST_REQUEST, 
    GET_CHOUREY_TWO_LIST_RESET, 
    GET_CHOUREY_TWO_LIST_SUCCESS, 
    MULTIPLE_CHOUREY_ONE_DELETE_FAIL, 
    MULTIPLE_CHOUREY_ONE_DELETE_REQUEST, 
    MULTIPLE_CHOUREY_ONE_DELETE_RESET, 
    MULTIPLE_CHOUREY_ONE_DELETE_SUCCESS, 
    MULTIPLE_CHOUREY_TWO_DELETE_FAIL, 
    MULTIPLE_CHOUREY_TWO_DELETE_REQUEST, 
    MULTIPLE_CHOUREY_TWO_DELETE_RESET, 
    MULTIPLE_CHOUREY_TWO_DELETE_SUCCESS, 
    UPDATE_CHOUREY_ONE_FAIL, 
    UPDATE_CHOUREY_ONE_REQUEST,
    UPDATE_CHOUREY_ONE_RESET,
    UPDATE_CHOUREY_ONE_SUCCESS,
    UPDATE_CHOUREY_TWO_FAIL,
    UPDATE_CHOUREY_TWO_REQUEST,
    UPDATE_CHOUREY_TWO_RESET,
    UPDATE_CHOUREY_TWO_SUCCESS
} from "../contsants/choureyConstants";

export const getChoureyOneListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_CHOUREY_ONE_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_CHOUREY_ONE_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_CHOUREY_ONE_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_CHOUREY_ONE_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const choureyOneDetailReducer = (state = {}, action) => {
    switch (action.type) {
        case CHOUREY_ONE_DETAIL_REQUEST:
            return {
                loading: true
            }
        case CHOUREY_ONE_DETAIL_SUCCESS:
            return {
                loading: false,
                success: true,
                choureyOneInfo: action.payload
            };
        case CHOUREY_ONE_DETAIL_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const choureyOneDeleteReducer = (state = {}, action) => {
    switch (action.type) {
        case CHOUREY_ONE_DELETE_REQUEST:
            return {
                loading: true
            }
        case CHOUREY_ONE_DELETE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case CHOUREY_ONE_DELETE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case CHOUREY_ONE_DELETE_RESET:
            return {}
        default:
            return state;
    }
}

export const createChoureyOneReducer = (state = {}, action) => {
    switch (action.type) {
        case CREATE_CHOUREY_ONE_REQUEST:
            return {
                loading: true
            }
        case CREATE_CHOUREY_ONE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case CREATE_CHOUREY_ONE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case CREATE_CHOUREY_ONE_RESET:
            return {}
        default:
            return state;
    }
}

export const choureyOneUpdateReducer = (state = {}, action) => {
    switch (action.type) {
        case UPDATE_CHOUREY_ONE_REQUEST:
            return {
                loading: true
            }
        case UPDATE_CHOUREY_ONE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case UPDATE_CHOUREY_ONE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case UPDATE_CHOUREY_ONE_RESET:
            return {}
        default:
            return state;
    }
}

export const getChoureyTwoListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_CHOUREY_TWO_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_CHOUREY_TWO_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_CHOUREY_TWO_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_CHOUREY_TWO_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const choureyTwoDetailReducer = (state = {}, action) => {
    switch (action.type) {
        case CHOUREY_TWO_DETAIL_REQUEST:
            return {
                loading: true
            }
        case CHOUREY_TWO_DETAIL_SUCCESS:
            return {
                loading: false,
                success: true,
                choureyTwoInfo: action.payload
            };
        case CHOUREY_TWO_DETAIL_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
}

export const choureyTwoDeleteReducer = (state = {}, action) => {
    switch (action.type) {
        case CHOUREY_TWO_DELETE_REQUEST:
            return {
                loading: true
            }
        case CHOUREY_TWO_DELETE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case CHOUREY_TWO_DELETE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case CHOUREY_TWO_DELETE_RESET:
            return {}
        default:
            return state;
    }
}

export const createChoureyTwoReducer = (state = {}, action) => {
    switch (action.type) {
        case CREATE_CHOUREY_TWO_REQUEST:
            return {
                loading: true
            }
        case CREATE_CHOUREY_TWO_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case CREATE_CHOUREY_TWO_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case CREATE_CHOUREY_TWO_RESET:
            return {}
        default:
            return state;
    }
}

export const choureyTwoUpdateReducer = (state = {}, action) => {
    switch (action.type) {
        case UPDATE_CHOUREY_TWO_REQUEST:
            return {
                loading: true
            }
        case UPDATE_CHOUREY_TWO_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case UPDATE_CHOUREY_TWO_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case UPDATE_CHOUREY_TWO_RESET:
            return {}
        default:
            return state;
    }
}

export const multipleChoureyOneDeleteReducer = (state = {}, action) => {
    switch (action.type) {
        case MULTIPLE_CHOUREY_ONE_DELETE_REQUEST:
            return {
                loading: true
            }
        case MULTIPLE_CHOUREY_ONE_DELETE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case MULTIPLE_CHOUREY_ONE_DELETE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case MULTIPLE_CHOUREY_ONE_DELETE_RESET:
            return {}
        default:
            return state;
    }
}

export const multipleChoureyTwoDeleteReducer = (state = {}, action) => {
    switch (action.type) {
        case MULTIPLE_CHOUREY_TWO_DELETE_REQUEST:
            return {
                loading: true
            }
        case MULTIPLE_CHOUREY_TWO_DELETE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case MULTIPLE_CHOUREY_TWO_DELETE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case MULTIPLE_CHOUREY_TWO_DELETE_RESET:
            return {}
        default:
            return state;
    }
}