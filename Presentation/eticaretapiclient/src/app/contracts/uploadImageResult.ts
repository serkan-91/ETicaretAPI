export class UploadImageResult {
  fileName: string;
  pathOrContainerName: string;

  constructor(fileName: string, pathOrContainerName: string) {
    this.fileName = fileName;
    this.pathOrContainerName = pathOrContainerName;
  }
}

export class UploadImageResults {
  uploadImages: UploadImageResult[];

  constructor(uploadImages: UploadImageResult[]) {
    this.uploadImages = uploadImages;
  }
}
