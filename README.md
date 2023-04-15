# Nastran Input Generator for Stiffened Panels

Description:

The Nastran Input Generator for Stiffened Panels is a C# application designed to generate input files for the Nastran finite element analysis (FEA) software specifically tailored for stiffened panel structures. The application provides a user-friendly interface to define the geometric and material properties of the stiffened panel and automatically generates Nastran input files that can be used for structural analysis.

![Nastran stiffened Panels](/Images/Plate_nat_freq.png)


Features:

Geometric Definition: The application allows users to define the geometry of the stiffened panel by specifying parameters such as panel length, width, thickness, and the locations, dimensions, and orientations of stiffeners.

Material Properties: The application allows users to define the material properties of the stiffened panel, including the material type, Young's modulus, Poisson's ratio, and thickness of the panel and stiffeners.

Nastran Input Generation: The application generates Nastran input files in the required format based on the defined geometry and material properties. The generated input files include information such as node coordinates, element connectivity, element properties, material properties, and boundary conditions, all tailored for a stiffened panel structure.

File Export: The application allows users to export the generated Nastran input files in a format compatible with the Nastran software, making it easy to import and use in Nastran for further analysis.

Benefits:

- Simplifies the process of creating Nastran input files for stiffened panel structures, reducing the manual effort and potential for errors.
- Provides a user-friendly interface for defining the geometric and material properties of the stiffened panel.
- Offers customization options to tailor the input files for specific analysis requirements.
- Enhances productivity and efficiency in generating Nastran input files for stiffened panel structures, making it suitable for engineering and aerospace applications.

Overall, the Nastran Input Generator for Stiffened Panels is a powerful C# application that simplifies the process of creating Nastran input files for stiffened panel structures, saving time and effort in the analysis and design of these structures.

Example after importing the model to Nastran preprocessors

![Example 1](/Images/Plate_nat_freq_example_1.png)

![Example 2](/Images/Plate_nat_freq_example_2.png)

![Example 3](/Images/Plate_nat_freq_example_3.png)

# Submerged Panels

The natural frequency in submerged condition can be expressed as

F<sub>L</sub> = f<sub>n</sub> &Delta;

where:

F<sub>L</sub> = natural frequency of panel in submerged condition, Hz<br>
f<sub>n</sub> = natural frequency in air, Hz<br>
&Delta; = reduction factor<br>

&Delta; is expressed as:<br>
&Delta; = 1 / √(1 + ε)

where:

ε = (Cρ<sub>L</sub> a) / (πρ<sub>p</sub> t<sub>e</sub> √(1 + (a/l)<sup>2</sup>))

where:
- C = 1 if liquid is on one side <br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2 if liquid is on both sides,
- ρ<sub>L</sub> = density of liquid (kg/m),
- ρ<sub>p</sub> = density of panel material (kg/m),
- t<sub>e</sub> = equivalent thickness of panel,
- a, l = breadth and length of the panel

For steel panel vibrating in water the value of ε becomes:

ε = (0.04 C a) / ( t<sub>e</sub> √(1 + (a/l)<sup>2</sup>))





___________________________________________________ END _____________________________________________________________
