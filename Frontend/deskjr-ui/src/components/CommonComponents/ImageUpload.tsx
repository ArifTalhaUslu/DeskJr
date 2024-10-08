import React, { useEffect, useState } from "react";

interface ImageUploadProps {
  onUpload?: (base64Image: string) => void;
  imageBase64?: string;
}

const ImageUpload: React.FC<ImageUploadProps> = ({ onUpload, imageBase64 }) => {
  const [localBase64, setLocalBase64] = useState<string | undefined>(imageBase64);

  useEffect(() => {
    setLocalBase64(imageBase64);
  }, [imageBase64]);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFile = event.target.files?.[0];
    if (selectedFile) {
      const reader = new FileReader();
      reader.onloadend = () => {
        const base64Image = reader.result as string;
        setLocalBase64(base64Image);

        if (onUpload) {
          onUpload(base64Image);
        }
      };
      reader.readAsDataURL(selectedFile);
    }
  };

  return (
    <div>
      {localBase64 && (
        <img
          src={localBase64}
          alt="Uploaded"
          style={{ width: "50px", height: "50px", borderRadius: "50%" }}
        />
      )}
      <input
        className="m-2"
        type="file"
        accept="image/*"
        onChange={handleFileChange}
      />
    </div>
  );
};


export default ImageUpload;
