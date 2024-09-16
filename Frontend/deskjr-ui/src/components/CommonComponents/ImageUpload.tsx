import React, { useState } from "react";

interface ImageUploadProps {
  onUpload: (base64Image: string) => void;
  imageBase64?: string;
}

const ImageUpload: React.FC<ImageUploadProps> = ({ onUpload, imageBase64 }) => {
  const [file, setFile] = useState<File | null>(null);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFile = event.target.files?.[0];
    if (selectedFile) {
      const reader = new FileReader();
      reader.onloadend = () => {
        const base64Image = reader.result as string;
        setFile(selectedFile);
        onUpload(base64Image);
      };
      reader.readAsDataURL(selectedFile);
    }
  };
  return (
    <div>
      {imageBase64 ? (
        <img
          src={imageBase64}
          alt="Uploaded"
          style={{ width: "50px", height: "50px", borderRadius: "50%" }}
        />
      ) : (
        <input type="file" accept="image/*" onChange={handleFileChange} />
      )}
    </div>
  );
};
export default ImageUpload;
