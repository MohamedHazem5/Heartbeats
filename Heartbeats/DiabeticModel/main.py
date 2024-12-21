import numpy as np
from PIL import Image
import torch
import torch.nn as nn
from torchvision import models, transforms

class DiabeticDetectionModel:
    def __init__(self, model_path, device=None):
        self.device = device or torch.device('cuda' if torch.cuda.is_available() else 'cpu')
        self.transform = transforms.Compose([
            transforms.Resize((224, 224)),
            transforms.RandomHorizontalFlip(p=0.5),
            transforms.RandomVerticalFlip(p=0.5),
            transforms.RandomRotation(30),
            transforms.ToTensor(),
            transforms.Normalize(mean=[0.485, 0.456, 0.406], std=[0.229, 0.224, 0.225]),
        ])
        self.class_labels = {0: 'Diabetic Retinopathy', 1: 'No Diabetic Retinopathy'}
        self.model = self._load_model(model_path)
    
    def _load_model(self, model_path):
        model = TransferNet()
        model.load_state_dict(torch.load(model_path))
        model.to(self.device)
        return model
    
    def predict(self, image_path):
        self.model.eval()
        img = Image.open(image_path).convert('RGB')
        img = self.transform(img).unsqueeze(0).to(self.device)
        with torch.no_grad():
            output = self.model(img)
            probabilities = torch.softmax(output, dim=1)
            predicted_class = torch.argmax(probabilities, dim=1).item()
        return predicted_class, probabilities[0].cpu().numpy()

class TransferNet(nn.Module):
    def __init__(self, num_classes=2):
        super(TransferNet, self).__init__()
        resnet = models.resnet18(pretrained=True)
        self.features = nn.Sequential(*list(resnet.children())[:-1])
        self.fc = nn.Linear(resnet.fc.in_features, num_classes)

    def forward(self, x):
        x = self.features(x)
        x = x.view(x.size(0), -1)
        x = self.fc(x)
        return x

if __name__ == "__main__":
    model_path = "model.pth"
    image_path = "4.png"
    
    diabetic_detection_model = DiabeticDetectionModel(model_path)
    predicted_class, probabilities = diabetic_detection_model.predict(image_path)
    class_name = diabetic_detection_model.class_labels[predicted_class]
    print(f"Predicted class: {class_name}, Probabilities: {probabilities}")





