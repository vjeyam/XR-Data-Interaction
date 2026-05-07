# XR Data Interaction

A Unity + Meta Quest 3 XR prototype for immersive visual analytics. Users can grab a dataset object, place it on a hologram-style table, and interact with a 3D data visualization using hand tracking and gesture-based controls.

![Project Description](docs/screenshots/project-description.png)

---

## Table of Contents

- [Overview](#overview)
- [Project Objective](#project-objective)
- [Current Prototype](#current-prototype)
- [Core Features](#core-features)
- [Dataset](#dataset)
- [How to Run](#how-to-run)
- [Known Issues](#known-issues)
- [Future Work](#future-work)

---

## Overview

During user studies of VR/AR systems, complex multidimensional datasets are often collected. This project explores how those datasets can be visualized and interacted with inside an XR environment.

The goal is to create an immersive visual analytics prototype where users can inspect data spatially — moving beyond traditional 2D charts. The interaction style is inspired by holographic scenes from Iron Man, where a user manipulates floating information using hand gestures and spatial controls.

---

## Project Objective

The project builds a visual analysis pipeline for Unity in VR that supports data generated during user studies.

This implementation focuses on:

- Loading CSV datasets into Unity
- Rendering selected dataset columns as a 3D scatterplot
- Allowing the user to spawn the visualization by placing a dataset cube onto a hologram table
- Supporting Meta Quest 3 hand tracking
- Adding gesture-based plot interaction
- Prototyping variable selection through grabbable variable cubes

---

## Current Prototype

The current prototype supports one active dataset: **Breast Cancer Wisconsin Dataset**.

The user can:

1. Grab the dataset cube.
2. Place it onto the hologram table.
3. Spawn a 3D scatterplot.
4. View a floating dataset title label.
5. Use hand gestures to scale and pause/rotate the plot.
6. Move variable cubes near the plot to prototype variable selection.

---

## Core Features

### Dataset Cube Interaction

A grabbable dataset cube represents the active CSV dataset. When the cube enters the table drop zone, the dataset plot is generated.

### 3D CSV Plotting

Custom C# scripts handle CSV loading (`CSVLoader.cs`) and 3D rendering (`CSVPointPlot.cs`). The current plot maps CSV columns to 3D coordinates:

```text
X = radius_mean
Y = texture_mean
Z = area_mean
Color = diagnosis
```

### Hologram Table

The visualization spawns above a hologram-style table and slowly rotates in place.

### Hand Gesture Controls

Basic hand gesture interactions are implemented using hand transform positions:

| Gesture | Action |
|---|---|
| Hands apart | Scale plot up |
| Hands together | Scale plot down |
| Hand near head | Pause / resume rotation |

### Variable Cube Prototype

Two variables are currently implemented: radius_mean and area_mean. Using hand gestures, you can select them and view an updated plot showing the correlation between those variables and breast cancer diagnosis.

---

## Dataset

The current working dataset is the Breast Cancer Wisconsin Dataset, stored at:

```text
Assets/Resources/Datasets/breast_cancer_wisconsin.csv
```

`CSVLoader.cs` parses the entire dataset and `CSVPointPlot.cs` generates the 3D scatterplot.

---

## How to Run

### Dependencies

This project requires:

- Unity 2022.3 LTS
- Android Build Support
- OpenJDK
- Android SDK & NDK Tools
- XR Plugin Management
- Oculus XR / Meta XR support
- Meta Quest 3 headset

**Installing Meta XR SDKs**

1. In the Unity Asset Store, search for **Meta XR Core SDK** and add it to your assets. Repeat for **Meta XR Interaction SDK**. Restart Unity if prompted.

2. In Unity, go to **Window -> Package Manager -> My Assets** and install the Meta XR Core SDK. Enable the **Meta XR Feature Set** if prompted.

3. Return to **Window -> Package Manager -> My Assets** and install the Meta XR Interaction SDK. When asked to select an Interaction SDK, choose Use **OpenXR** Hand.

### Setup

1. In Unity Hub, go to Installs → Unity 2022.3 LTS → Manage → Add Modules and install:

    - Android Build Support
    - OpenJDK
    - Android SDK & NDK Tools

2. Open the Unity project.
3. Go to **Edit -> Project Settings -> XR Plug-in Management**
4. Under **XR Plug-in Management -> Android**, enable `OpenXR`.
5. Open the Project Validation window and click **Fix All** to resolve common build setting issues.
6. Go to **File -> Build Settings** and set the platform to `Android`.
7. Add `VRDatasetTable` scene to **Scenes in Build**.
8. Connect the Meta Quest 3 headset and click **Build and Run**.

---

## Known Issues

Quick disclaimer: This Unity project was implemented on a Mac M2 chip, so there was no testing on any Windows or Linux computers.

- Variable cube selection is partially implemented and may be inconsistent depending on cube orientation and trigger collision behavior.
- Plot axes may occasionally regenerate incorrectly if variable selection occurs while the plot is rotating or being manipulated.
- Only one dataset is currently fully integrated: Breast Cancer Wisconsin.n.
- Gesture recognition is based on hand transform positions, not ML-based detection, so results are approximate.
- Variable cube snap-back behavior is still experimental.
- Some interaction objects may need manual position tuning depending on headset setup and available play space.

---

## Future Work

Some improvements can include:

- Support for multiple datasets
- More reliable variable selection using dedicated X/Y/Z drop zones
- Ability to dynamically add/remove variables from the plot
- Robust UI panels for selected variables, dataset metadata, and interaction hints
- Additional 3D plot types beyond scatterplots
