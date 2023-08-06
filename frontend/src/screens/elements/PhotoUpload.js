import React, {useCallback} from 'react'
import { useDropzone } from "react-dropzone";
import { useTranslation } from 'react-i18next'

const PhotoUpload = ({setFiles, files}) => {

    const {t} = useTranslation()

    const onDrop = useCallback((acceptedFiles) => {
        setFiles(
            acceptedFiles.map((file) =>
            Object.assign(file, {
                preview: URL.createObjectURL(file),
            })
            )
        );
    }, []);

    const { getRootProps, getInputProps } = useDropzone({
        onDrop,
        accept: "image/*",
        multiple: true,
    });

    const thumbs = files.map((file) => (
        <div className="w-80 h-80 p-2 rounded-md">
            <img src={file.preview} className="object-cover w-full h-full rounded-md" />
        </div>
    ));

    const Display = () => {
        return (
            <>
                {files.length == 0 ?
                    (<p>Drag and drop some files here, or click to select files</p>)
                : files.length == 1 ?
                    (files.map((file) => (
                            <p>{file.name}</p>
                        )
                    )
                    )
                :
                    (<p>Multiple image selected</p>)
                }
            </>
        )
    }

    return (
        <div className="flex flex-col space-y-4 mt-40">
            <div className="flex items-start justify-between p-2 border-b border-solid border-slate-200 rounded-t">
                <h3 className="text-2xl font-semibold">
                {t("Upload Photo")}
                </h3>
            </div>
            <div className="flex flex-wrap">{thumbs}</div>
            <div
                {...getRootProps()}
                className="p-4 bg-gray-100 w-1/3 border-2 border-dashed border-gray-400 flex justify-center items-center"
            >
                <input {...getInputProps()} />
                <Display />
            </div>
        </div>
    )
}

export default PhotoUpload