import { CREATE_COMMON_CONTENTS_FAIL, CREATE_COMMON_CONTENTS_REQUEST, CREATE_COMMON_CONTENTS_RESET, CREATE_COMMON_CONTENTS_SUCCESS, DELETE_COMMON_CONTENTS_FAIL, DELETE_COMMON_CONTENTS_REQUEST, DELETE_COMMON_CONTENTS_RESET, DELETE_COMMON_CONTENTS_SUCCESS, GET_COMMON_CONTENTS_DETAIL_FAIL, GET_COMMON_CONTENTS_DETAIL_REQUEST, GET_COMMON_CONTENTS_DETAIL_RESET, GET_COMMON_CONTENTS_DETAIL_SUCCESS, GET_COMMON_CONTENTS_LIST_FAIL, GET_COMMON_CONTENTS_LIST_REQUEST, GET_COMMON_CONTENTS_LIST_RESET, GET_COMMON_CONTENTS_LIST_SUCCESS, UPDATE_COMMON_CONTENTS_FAIL, UPDATE_COMMON_CONTENTS_REQUEST, UPDATE_COMMON_CONTENTS_RESET, UPDATE_COMMON_CONTENTS_SUCCESS } from "../contsants/commonContentsConstants";


export const getCommonContentsListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_COMMON_CONTENTS_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_COMMON_CONTENTS_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_COMMON_CONTENTS_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_COMMON_CONTENTS_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const createCommonContentsReducer = (state = {}, action) => {
    switch (action.type) {
        case CREATE_COMMON_CONTENTS_REQUEST:
            return {
                loading: true
            }
        case CREATE_COMMON_CONTENTS_SUCCESS:
            return {
                loading: false,
                success: true,
            };
        case CREATE_COMMON_CONTENTS_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case CREATE_COMMON_CONTENTS_RESET:
            return {}
        default:
            return state;
    }
}

export const updateCommonContentsReducer = (state = {}, action) => {
    switch (action.type) {
        case UPDATE_COMMON_CONTENTS_REQUEST:
            return {
                loading: true
            }
        case UPDATE_COMMON_CONTENTS_SUCCESS:
            return {
                loading: false,
                success: true,
            };
        case UPDATE_COMMON_CONTENTS_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case UPDATE_COMMON_CONTENTS_RESET:
            return {}
        default:
            return state;
    }
}

export const getCommonContentsDetailReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_COMMON_CONTENTS_DETAIL_REQUEST:
            return {
                loading: true
            }
        case GET_COMMON_CONTENTS_DETAIL_SUCCESS:
            return {
                loading: false,
                success: true,
                detail: action.payload 
            };
        case GET_COMMON_CONTENTS_DETAIL_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_COMMON_CONTENTS_DETAIL_RESET:
            return {}
        default:
            return state;
    }
}

export const deleteCommonContentsReducer = (state = {}, action) => {
    switch (action.type) {
        case DELETE_COMMON_CONTENTS_REQUEST:
            return {
                loading: true
            }
        case DELETE_COMMON_CONTENTS_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case DELETE_COMMON_CONTENTS_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case DELETE_COMMON_CONTENTS_RESET:
            return {}
        default:
            return state;
    }
}