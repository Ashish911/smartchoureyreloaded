import { ASSIGN_DEVICE_MAPPING_LIST_FAIL, ASSIGN_DEVICE_MAPPING_LIST_REQUEST, ASSIGN_DEVICE_MAPPING_LIST_RESET, ASSIGN_DEVICE_MAPPING_LIST_SUCCESS, DELETE_DEVICE_MAPPING_LIST_FAIL, DELETE_DEVICE_MAPPING_LIST_REQUEST, DELETE_DEVICE_MAPPING_LIST_RESET, DELETE_DEVICE_MAPPING_LIST_SUCCESS, GET_DEVICE_MAPPING_LIST_FAIL, GET_DEVICE_MAPPING_LIST_REQUEST, GET_DEVICE_MAPPING_LIST_RESET, GET_DEVICE_MAPPING_LIST_SUCCESS, UPDATE_DEVICE_MAPPING_LIST_FAIL, UPDATE_DEVICE_MAPPING_LIST_REQUEST, UPDATE_DEVICE_MAPPING_LIST_RESET, UPDATE_DEVICE_MAPPING_LIST_SUCCESS } from "../contsants/deviceConstants";

export const getDeviceListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_DEVICE_MAPPING_LIST_REQUEST:
            return {
                loading: true
            }
        case GET_DEVICE_MAPPING_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_DEVICE_MAPPING_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_DEVICE_MAPPING_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const updateDeviceMappingReducer = (state = {}, action) => {
    switch (action.type) {
        case UPDATE_DEVICE_MAPPING_LIST_REQUEST:
            return {
                loading: true
            }
        case UPDATE_DEVICE_MAPPING_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case UPDATE_DEVICE_MAPPING_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case UPDATE_DEVICE_MAPPING_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const assignDeviceMappingReducer = (state = {}, action) => {
    switch (action.type) {
        case ASSIGN_DEVICE_MAPPING_LIST_REQUEST:
            return {
                loading: true
            }
        case ASSIGN_DEVICE_MAPPING_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case ASSIGN_DEVICE_MAPPING_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case ASSIGN_DEVICE_MAPPING_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const deleteDeviceMappingReducer = (state = {}, action) => {
    switch (action.type) {
        case DELETE_DEVICE_MAPPING_LIST_REQUEST:
            return {
                loading: true
            }
        case DELETE_DEVICE_MAPPING_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case DELETE_DEVICE_MAPPING_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case DELETE_DEVICE_MAPPING_LIST_RESET:
            return {}
        default:
            return state;
    }
}