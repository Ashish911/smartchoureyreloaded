import React, {useCallback, useEffect} from 'react'
import { useDropzone } from "react-dropzone";
import { useTranslation } from 'react-i18next'

const PdfUpload = ({  pdfFiles, setPdfFiles }) => {

    const {t} = useTranslation()

    const onDrop = useCallback(
        (acceptedFiles) => {
            const pdfFiles = acceptedFiles.filter((file) =>
                Object.assign(file, {
                    preview: URL.createObjectURL(file)
                })
            );
            setPdfFiles(pdfFiles);
        },
        []
    );

    const { getRootProps, getInputProps } = useDropzone({ onDrop });
    
    const Display = () => {
        return (
            <>
                {pdfFiles.length == 0 ?
                    (<p>Drag and drop some files here, or click to select files</p>)
                : pdfFiles.length == 1 ?
                    (pdfFiles.map((file) => (
                            <p>{file.name}</p>
                        )
                    )
                    )
                :
                    (<p>Multiple pdf selected</p>)
                }
            </>
        )
    }

    return (
        <div className="flex flex-col space-y-4 mt-40">
            <div className="flex items-start justify-between p-2 border-b border-solid border-slate-200 rounded-t">
                <h3 className="text-2xl font-semibold">
                {t("Upload PDF")}
                </h3>
            </div>
            {pdfFiles.length > 0 && (
            <div className="mt-4">
                {pdfFiles.map((file) => (
                    <object data={file.preview} type="application/pdf">
                        <p>PDF preview is not available.</p>
                    </object>
                ))}
            </div>
            )}
            <div
                {...getRootProps()}
                className="p-4 bg-gray-100 w-1/3 border-2 border-dashed border-gray-400 flex justify-center items-center"
            >
                <input {...getInputProps()} accept="application/pdf" multiple />
                <Display />
            </div>
        </div>
    )
}

export default PdfUpload