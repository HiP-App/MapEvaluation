using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using BruTile.Predefined;
using Mapsui;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Providers;
using Mapsui.Styles;

namespace MapTest
{
    public static class CommonMap
    {

        public static Map CreateMap()
        {
            var map = new Map();

            map.Layers.Add(new TileLayer(KnownTileSources.Create()) { Name = "OSM" });
             map.Layers.Add(CreateLineStringLayer(CreateLineStringStyle()));
            map.Viewport.Resolution = 1000;

            return map;
        }

        public static ILayer CreateLineStringLayer(IStyle style = null)
        {
            return new MemoryLayer
            {
                DataSource = new MemoryProvider(new Feature
                {
                    Styles = new Reminiscence.Collections.List<IStyle> { style },
                    Geometry =

                        new LineString(new[]
                            {
                                new Point(0, 0),
                                new Point(1000000, 0),
                                new Point(1000000, 1000000),
                                new Point(2000000, 1000000),
                                new Point(2000000, 2000000),
                                new Point(3000000, 2000000),
                                new Point(3000000, 3000000),
                                new Point(4000000, 3000000),
                                new Point(4000000, 4000000),
                                new Point(5000000, 4000000)
                            }
                        )
                }
                ),
                Name = "LineStringLayer",
                Style = style
            };
        }

        public static IStyle CreateLineStringStyle()
        {
            return new VectorStyle
            {
                Fill = null,
                Outline = null,
                Line = { Color = Color.Red, Width = 4 }
            };
        }
    }
}