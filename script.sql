USE [ParkingLotManager]
GO

INSERT INTO [Company]
VALUES 
    ('Parking Lot', '30873163999911', 'Park street', 'Sao Paulo', '40200123', '32287778888', 5, 5)

INSERT INTO [Vehicle]
VALUES 
    ('BBB4444', 'Ford', 'Mustang', 'Deep Blue', 1, GETDATE(), GETDATE(), 'Parking Lot', 1),
    ('BBB3333', 'Ford', 'Ranger', 'Black', 1, GETDATE(), GETDATE(), 'Parking Lot', 1),
    ('CCC1111', 'Ferrari', 'Spider', 'Red', 1, GETDATE(), GETDATE(), 'Parking Lot', 1),
    ('DDD7777', 'Mercedes', 'C-Class', 'Gray', 1, GETDATE() ,GETDATE(), 'Parking Lot', 1),
    ('NNN2323', 'Audi', 'A3', 'White', 1, GETDATE(), GETDATE(), 'Parking Lot', 1),
    ('III8888', 'Honda', 'CBR 1000 RR', 'Purple', 2, GETDATE(), GETDATE(), 'Parking Lot', 1),
    ('UUU7654', 'Kawasaki', 'Ninja ZX 10R', 'Green', 2, GETDATE(), GETDATE(), 'Parking Lot', 1)