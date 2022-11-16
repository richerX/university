package graph;

public class Main {
    public static void main(String[] args) throws DAGConstraintException {
        System.out.println("Main started!\n");

        Space space = new Space(new Coord2D(0, 0));
        Origin origin1 = new Origin(new Coord2D(0, 0));
        Origin origin2 = new Origin(new Coord2D(1, 1));
        Origin origin3 = new Origin(new Coord2D(-2, -2));
        Point point1 = new Point(new Coord2D(1, 1));
        Point point2 = new Point(new Coord2D(2, 2));

        space.add(origin1);
        space.add(origin2);
        space.add(origin3);

        origin1.add(point1);
        origin1.add(point2);

        System.out.println(space);

        System.out.println("Main finished!");
    }
}
