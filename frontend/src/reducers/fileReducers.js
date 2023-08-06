import { DELETE_MEDIA_FILE_FAIL, DELETE_MEDIA_FILE_REQUEST, DELETE_MEDIA_FILE_RESET, DELETE_MEDIA_FILE_SUCCESS } from "../contsants/fileConstants";

export const deleteFileReducer = (state = {}, action) => {
    switch (action.type) {
        case DELETE_MEDIA_FILE_REQUEST:
            return {
                loading: true
            }
        case DELETE_MEDIA_FILE_SUCCESS:
            return {
                loading: false,
                success: true,
                type: action.payload.type,
                uniqueId: action.payload.fileId
            };
        case DELETE_MEDIA_FILE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case DELETE_MEDIA_FILE_RESET:
            return {}
        default:
            return state;
    }
}